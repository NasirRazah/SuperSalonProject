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

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ReportSchdEntry.xaml
    /// </summary>
    public partial class frm_ReportSchdEntry : Window
    {
        public frm_ReportSchdEntry()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private frm_ReportSchedule frmBrowse;

        private int intID;

        private string strReportTag;

        public string ReportTag
        {
            get { return strReportTag; }
            set { strReportTag = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public frm_ReportSchedule BrowseF
        {
            get { return frmBrowse; }
            set { frmBrowse = value; }
        }

        private void RgRepType_1_Checked(object sender, RoutedEventArgs e)
        {
            if (strReportTag == "17")
            {
                if (rgRepType_3.IsChecked == true)
                {
                    lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Visible;
                }
                else
                {
                    lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (strReportTag == "11")
            {
                Title.Text = "Sales By SKU";
                lbsort.Text = "SKU";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("Family");
                cmbsort.Items.Add("UPC");
                cmbsort.Items.Add("Season");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");
                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Hidden;
            }

            if (strReportTag == "12")
            {
                Title.Text = "Sales By Department";
                lbsort.Text = "Department";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("SKU");
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("Family");
                cmbsort.Items.Add("UPC");
                cmbsort.Items.Add("Season");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");
                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Visible;
                rgRepType_1.IsChecked = true;
            }

            if (strReportTag == "13")
            {
                Title.Text = "Sales By POS Screen Category";
                lbsort.Text = "POS Screen Category";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("SKU");
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("Family");
                cmbsort.Items.Add("UPC");
                cmbsort.Items.Add("Season");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");
                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Visible;
                rgRepType_1.IsChecked = true;
            }

            if (strReportTag == "14")
            {
                Title.Text = "Sales By Family";
                lbsort.Text = "Family";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("SKU");
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("UPC");
                cmbsort.Items.Add("Season");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");
                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Visible;
                rgRepType_1.IsChecked = true;
            }

            if (strReportTag == "15")
            {
                Title.Text = "Sales By Employee";
                lbsort.Text = "Employee";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("SKU");
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("UPC");
                cmbsort.Items.Add("Season");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");
                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Visible;
                rgRepType_1.IsChecked = true;
            }

            if (strReportTag == "16")
            {
                Title.Text = "Sales By Vendor";
                lbsort.Text = "Vendor";

                cmbsort.Items.Clear();
                cmbsort.Items.Add("SKU");
                cmbsort.Items.Add("Description");
                cmbsort.Items.Add("Vendor PN");
                cmbsort.Items.Add("On Hand Qty");
                cmbsort.Items.Add("Qty Sold");
                cmbsort.Items.Add("Cost");
                cmbsort.Items.Add("Revenue");
                cmbsort.Items.Add("Profit");
                cmbsort.Items.Add("Margin %");

                cmbsort.SelectedIndex = 0;
                lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Visible;
                rgRepType_1.IsChecked = true;
            }

            if (strReportTag == "17")
            {
                Title.Text = "Sales By Customer";
                rgRepType_3.IsChecked = true;
                rgRepType_3.Visibility = rgRepType_4.Visibility = rgRepType_5.Visibility = rgRepType_6.Visibility = Visibility.Visible;
                rgRepType_1.Visibility = rgRepType_2.Visibility = Visibility.Hidden;
            }

            if (strReportTag == "18")
            {
                Title.Text = "Monthly Sales";
               
                label4.Visibility = Visibility.Hidden;
                pnlSortBy.Visibility = Visibility.Hidden;
                rgRepType_1.Visibility = rgRepType_2.Visibility = rgRepType_3.Visibility = rgRepType_4.Visibility = rgRepType_5.Visibility = rgRepType_6.Visibility = Visibility.Hidden;
            }

            if (strReportTag == "19")
            {
                Title.Text = "Sales by Day of Week";
                label4.Visibility = Visibility.Hidden;
                pnlSortBy.Visibility = Visibility.Hidden;
                rgRepType_1.Visibility = rgRepType_2.Visibility = rgRepType_3.Visibility = rgRepType_4.Visibility = rgRepType_5.Visibility = rgRepType_6.Visibility = Visibility.Hidden;

            }

            if (strReportTag == "2")
            {
                Title.Text = "Sales Summary";

                label4.Visibility = Visibility.Hidden;
                pnlSortBy.Visibility = Visibility.Hidden;
                rgRepType_1.Visibility = rgRepType_2.Visibility = rgRepType_3.Visibility = rgRepType_4.Visibility = rgRepType_5.Visibility = rgRepType_6.Visibility = Visibility.Hidden;

            }



            if (strReportTag == "3")
            {
                Title.Text = "Dashboard - Sales";

                label4.Visibility = Visibility.Hidden;
                pnlSortBy.Visibility = Visibility.Hidden;
                rgRepType_1.Visibility = rgRepType_2.Visibility = rgRepType_3.Visibility = rgRepType_4.Visibility = rgRepType_5.Visibility = rgRepType_6.Visibility = Visibility.Hidden;

            }

            if (intID > 0)
            {
                PosDataObject.Setup obj = new PosDataObject.Setup();
                obj.Connection = SystemVariables.Conn;
                DataTable dtbl = obj.GetReportEntryRecord(intID);
                string tg = "";
                string rptype = "";
                string op1 = "";
                string op1ty = "";
                string op2 = "";
                string op2ty = "";
                string op3 = "";
                string op3ty = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    cmbDt.Text = dr["DateRange"].ToString();
                    tg = dr["ReportTag"].ToString();
                    rptype = dr["ReportType"].ToString();
                    op1 = dr["ReportSortOption_1"].ToString();
                    op1ty = dr["ReportSortOption_1_Type"].ToString();
                    op2 = dr["ReportSortOption_2"].ToString();
                    op2ty = dr["ReportSortOption_2_Type"].ToString();
                    op3 = dr["ReportSortOption_3"].ToString();
                    op3ty = dr["ReportSortOption_3_Type"].ToString();
                }
                dtbl.Dispose();

                rgRepType_1.IsEnabled = rgRepType_2.IsEnabled = rgRepType_3.IsEnabled = rgRepType_4.IsEnabled = rgRepType_5.IsEnabled= rgRepType_6.IsEnabled = false;

                if ((tg == "12") || (tg == "13") || (tg == "14") || (tg == "15") || (tg == "16"))
                {
                    if (rptype == "Detail")
                    {
                        rgRepType_1.IsChecked = true;
                    }
                    else
                    {
                        rgRepType_2.IsChecked = true;
                    }
                   
                    lbsort.Text = op1;
                    if (op1ty == "A")
                    {
                        rgsort_1.IsChecked = true;
                    }
                    else
                    {
                        rgsort_2.IsChecked = true;
                    }

                    cmbsort.Text = op3;

                    if (op3ty == "A")
                    {
                        rgsort1_1.IsChecked = true;
                    }
                    else
                    {
                        rgsort1_2.IsChecked = true;
                    }


                }

                if (tg == "17")
                {
                    if (rptype == "Dept")
                    {
                        rgRepType_3.IsChecked = true;
                    }
                    else if (rptype == "SKU")
                    {
                        rgRepType_4.IsChecked = true;
                    }
                    else if (rptype == "date")
                    {
                        rgRepType_5.IsChecked = true;
                    }
                    else
                    {
                        rgRepType_6.IsChecked = true;
                    }

                    if (rptype == "Dept")
                    {
                        lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lbsort2.Visibility = rgsort2_1.Visibility = rgsort2_2.Visibility = Visibility.Hidden;
                    }

                    lbsort.Text = op1;
                    if (op1ty == "A")
                    {
                        rgsort_1.IsChecked = true;
                    }
                    else
                    {
                        rgsort_2.IsChecked = true;
                    }
                   
                    lbsort2.Text = op2;
                    if (op2ty == "A")
                    {
                        rgsort2_1.IsChecked = true;
                    }
                    else
                    {
                        rgsort2_2.IsChecked = true;
                    }
                   
                    cmbsort.Text = op3;
                    if (op3ty == "A")
                    {
                        rgsort1_1.IsChecked = true;
                    }
                    else
                    {
                        rgsort1_2.IsChecked = true;
                    }
              
                }
            }
        }

        private bool ValidAll()
        {
            if (intID == 0)
            {
                PosDataObject.Setup objSetup = new PosDataObject.Setup();
                objSetup.Connection = SystemVariables.Conn;
                string ty = "";
                if ((strReportTag == "2") || (strReportTag == "3") || (strReportTag == "11") || (strReportTag == "18") || (strReportTag == "19"))
                {
                    ty = "";
                }

                if ((strReportTag == "12") || (strReportTag == "13") || (strReportTag == "14") || (strReportTag == "15") || (strReportTag == "16"))
                {
                    ty = rgRepType_1.IsChecked == true ? "Detail" : "Summary";
                }

                if (strReportTag == "17")
                {
                    ty = rgRepType_3.IsChecked == true ? "Dept" : (rgRepType_4.IsChecked == true ? "SKU" : (rgRepType_5.IsChecked == true ? "Date" : "Summary"));
                }

                if (objSetup.IsExistScheduledReport(strReportTag, ty))
                {
                    if (DocMessage.MsgConfirmation("A similar schedule already exists. Are you sure you want to add this schedule?") == MessageBoxResult.No) return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            bool ret = false;
            string err = "";
            int rcdCount = 0;
            string rptTitle = "";
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            objSetup.ID = intID;
            objSetup.LoginUserID = SystemVariables.CurrentUserID;

            // Get Maximum Report Entry of specific type
            if (intID == 0)
            {
                if ((strReportTag == "2") || (strReportTag == "3") || (strReportTag == "11") || (strReportTag == "18") || (strReportTag == "19"))
                {
                    rcdCount = objSetup.GetScheduledReportCount(strReportTag, "");
                }

                if ((strReportTag == "12") || (strReportTag == "13") || (strReportTag == "14") || (strReportTag == "15") || (strReportTag == "16"))
                {
                    rcdCount = objSetup.GetScheduledReportCount(strReportTag, (rgRepType_1.IsChecked == true ? "Detail" : "Summary"));
                }

                if (strReportTag == "17")
                {
                    rcdCount = objSetup.GetScheduledReportCount(strReportTag, (rgRepType_3.IsChecked == true ? "Dept" : (rgRepType_4.IsChecked == true ? "SKU" : (rgRepType_5.IsChecked == true ? "Date" : "Summary"))));
                }

                if (rcdCount == 1)
                {
                    if (strReportTag == "2") rptTitle = "sales summary.pdf";
                    if (strReportTag == "3") rptTitle = "dashboard - sales.pdf";
                    if (strReportTag == "11") rptTitle = "sales matrix by sku.pdf";
                    if (strReportTag == "12") rptTitle = "sales matrix by department" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + ".pdf";
                    if (strReportTag == "13") rptTitle = "sales matrix by pos category" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + ".pdf";
                    if (strReportTag == "14") rptTitle = "sales matrix by family" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + ".pdf";
                    if (strReportTag == "15") rptTitle = "sales matrix by employee" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + ".pdf";
                    if (strReportTag == "16") rptTitle = "sales matrix by vendor" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + ".pdf";

                    if (strReportTag == "17") rptTitle = "sales matrix by customer" + (rgRepType_3.IsChecked == true ? " - dept" : (rgRepType_4.IsChecked == true ? " - sku" : (rgRepType_5.IsChecked == true ? " - by date" : " - summary"))) + ".pdf";
                    if (strReportTag == "18") rptTitle = "sales matrix - monthly sales.pdf";
                    if (strReportTag == "19") rptTitle = "sales matrix - day of week.pdf";
                }

                if (rcdCount > 1)
                {
                    if (strReportTag == "2") rptTitle = "sales summary (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "3") rptTitle = "dashboard - sales (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "11") rptTitle = "sales matrix by sku (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "12") rptTitle = "sales matrix by department" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + " (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "13") rptTitle = "sales matrix by pos category" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + " (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "14") rptTitle = "sales matrix by family" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + " (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "15") rptTitle = "sales matrix by employee" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + " (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "16") rptTitle = "sales matrix by vendor" + (rgRepType_1.IsChecked == true ? " - detail" : " - summary") + " (" + (rcdCount - 1).ToString() + ").pdf";

                    if (strReportTag == "17") rptTitle = "sales matrix by customer" + (rgRepType_3.IsChecked == true ? " - dept" : (rgRepType_4.IsChecked == true ? " - sku" : (rgRepType_5.IsChecked == true ? " - by date" : " - summary"))) + " (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "18") rptTitle = "sales matrix - monthly sales (" + (rcdCount - 1).ToString() + ").pdf";
                    if (strReportTag == "19") rptTitle = "sales matrix - day of week (" + (rcdCount - 1).ToString() + ").pdf";
                }
            }

            if ((strReportTag == "2") || (strReportTag == "3") || (strReportTag == "18") || (strReportTag == "19"))
            {
                err = intID == 0 ? objSetup.InsertSimpleReportEntry(Title.Text, strReportTag, cmbDt.Text, rptTitle, rcdCount) : objSetup.UpdateSimpleReportEntry(cmbDt.Text);
            }

            if (strReportTag == "11")
            {
                err = intID == 0 ? objSetup.InsertComplexReportEntry(Title.Text, strReportTag, cmbDt.Text, "",
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), "", "", cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"), rptTitle, rcdCount) : objSetup.UpdateComplexReportEntry(cmbDt.Text, (rgRepType_1.IsChecked == true ? "Detail" : "Summary"),
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), "", "", cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"));
            }

            if ((strReportTag == "12") || (strReportTag == "13") || (strReportTag == "14") || (strReportTag == "15") || (strReportTag == "16"))
            {
                err = intID == 0 ? objSetup.InsertComplexReportEntry(Title.Text, strReportTag, cmbDt.Text, (rgRepType_1.IsChecked == true ? "Detail" : "Summary"),
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), "", "", cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"), rptTitle, rcdCount) : objSetup.UpdateComplexReportEntry(cmbDt.Text, (rgRepType_1.IsChecked == true ? "Detail" : "Summary"),
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), "", "", cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"));
            }

            if (strReportTag == "17")
            {
                err = intID == 0 ? objSetup.InsertComplexReportEntry(Title.Text, strReportTag, cmbDt.Text, (rgRepType_3.IsChecked == true ? "Dept" : (rgRepType_4.IsChecked == true ? "SKU" : (rgRepType_5.IsChecked == true ? "Date" : "Summary"))),
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), lbsort2.Text, (rgsort2_1.IsChecked == true ? "A" : "D"), cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"), rptTitle, rcdCount) : objSetup.UpdateComplexReportEntry(cmbDt.Text, (rgRepType_3.IsChecked == true ? "Dept" : (rgRepType_4.IsChecked == true ? "SKU" : (rgRepType_5.IsChecked == true ? "Date" : "Summary"))),
                    lbsort.Text, (rgsort_1.IsChecked == true ? "A" : "D"), lbsort2.Text, (rgsort2_1.IsChecked == true ? "A" : "D"), cmbsort.Text, (rgsort1_1.IsChecked == true ? "A" : "D"));
            }

            intID = objSetup.ID;

            return err == "";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidAll())
            {
                if (SaveData())
                {
                    frmBrowse.FetchData();
                    Close();
                }
            }
        }

        private void CmbDt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
