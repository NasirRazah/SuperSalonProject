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
using OfflineRetailV2;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ReportsUC.xaml
    /// </summary>
    public partial class frm_ReportsUC : UserControl
    {
        public frm_ReportsUC()
        {
            InitializeComponent();
            //repview.FocusedRowChanged += Repview_FocusedRowChanged;
        }

        public  void LoadData()
        {
            GetReportList();
            /*BuildTree();
            reptreelist.RefreshData();
            repview.Refresh();
            repview.ExpandAllNodes();
            repview.FocusedRowHandle = 1;*/
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdRep.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdRep, colID)));
            return intRecID;
        }

        private void GetReportList()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ReportID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ReportGroupID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ReportName", System.Type.GetType("System.String"));

            bool ScaleOnlyMode = false;
            string RegisterModule = GeneralFunctions.RegisteredModules();
            if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
            {
                ScaleOnlyMode = true;
            }


            TreeListNode rootNode = null;
            if (!ScaleOnlyMode)
            {
                
                dtbl.Rows.Add(new object[] {"1", "0", "Detail Report" });
                dtbl.Rows.Add(new object[] { "2", "0", "General Report" });
                dtbl.Rows.Add(new object[] { "4", "0", "Other Report ( Sales,Special Events etc )" });
                dtbl.Rows.Add(new object[] { "5", "0", "Repair List" });
                dtbl.Rows.Add(new object[] { "3", "0", "Sales by Product Report" });

                dtbl.Rows.Add(new object[] { "73", "1", "Parameters" });
                dtbl.Rows.Add(new object[] { "72", "1", "Points" });
                dtbl.Rows.Add(new object[] { "71", "1", "Visit" });

            }

            
            dtbl.Rows.Add(new object[] { "11", "3", "Detail Report" });
            dtbl.Rows.Add(new object[] { "12", "3", "General Report" });

            
            if (!ScaleOnlyMode)
            {
                dtbl.Rows.Add(new object[] { "21", "4", "Detail Report" });
                dtbl.Rows.Add(new object[] { "22", "4", "General Report" });
                dtbl.Rows.Add(new object[] { "28", "4", "Inventory Aging Report" });
                dtbl.Rows.Add(new object[] { "23", "4", "Kit Product Report" });
                dtbl.Rows.Add(new object[] { "24", "4", "Matrix Product Report" });
                dtbl.Rows.Add(new object[] { "209", "4", "Product Due for Expiry Report" });
                dtbl.Rows.Add(new object[] { "203", "4", "Product Family Report" });
                dtbl.Rows.Add(new object[] { "26", "4", "Product Tax, Price Report" });
                dtbl.Rows.Add(new object[] { "25", "4", "Serialized Product Report" });
                dtbl.Rows.Add(new object[] { "27", "4", "Stock Journal Report" });
                dtbl.Rows.Add(new object[] { "215", "4", "Stock Valuation Report" });

            }

            dtbl.Rows.Add(new object[] { "35", "5", "Absent Report" });
            dtbl.Rows.Add(new object[] { "33", "5", "Attendance Report" });
            dtbl.Rows.Add(new object[] { "31", "5", "Detail Report" });
            dtbl.Rows.Add(new object[] { "39", "5", "Employee Wage To Sales" });
            dtbl.Rows.Add(new object[] { "34", "5", "Late Report" });
            dtbl.Rows.Add(new object[] { "38", "5", "Payroll Export" });
            dtbl.Rows.Add(new object[] { "36", "5", "Payroll Report" });
            dtbl.Rows.Add(new object[] { "37", "5", "Special Events Report" });
           


            
            dtbl.Rows.Add(new object[] { "41", "6", "Purchase Order Report" });
            dtbl.Rows.Add(new object[] { "42", "6", "Receiving Report" });
            dtbl.Rows.Add(new object[] { "43", "6", "Reorder Report" });
            dtbl.Rows.Add(new object[] { "44", "6", "Vendor Minimum Order Report" });


            if (!ScaleOnlyMode)
            {
                dtbl.Rows.Add(new object[] { "56", "7", "Card Transaction Report" });
                dtbl.Rows.Add(new object[] { "509", "7", "Dashboard - Sales" });
                dtbl.Rows.Add(new object[] { "55", "7", "Discount Report" });
                dtbl.Rows.Add(new object[] { "550", "7", "Gift Aid Report" });
                dtbl.Rows.Add(new object[] { "54", "7", "Gift Cert. Activity Report" });
                dtbl.Rows.Add(new object[] { "53", "7", "Gift Cert. Balance Report" });
                //dtbl.Rows.Add(new object[] { "59", "7", "Matrix Item Sales Report" });
                
                dtbl.Rows.Add(new object[] { "58", "7", "Packing List" });
                dtbl.Rows.Add(new object[] { "60", "7", "Sale Price List" });
                dtbl.Rows.Add(new object[] { "501", "7", "Sales by Period" });
                dtbl.Rows.Add(new object[] { "51", "7", "Sales Matrix Report" });
                
                dtbl.Rows.Add(new object[] { "52", "7", "Sales Summary Report" });
                dtbl.Rows.Add(new object[] { "57", "7", "Suspended Order List" });
                dtbl.Rows.Add(new object[] { "511", "7", "Tax Analysis Report" });
            }
            else
            {
                dtbl.Rows.Add(new object[] { "60", "7", "Sale Price List" });
            }
            if (!ScaleOnlyMode)
            {
                if (Settings.RegPOSAccess == "Y")
                {
                    dtbl.Rows.Add(new object[] { "61", "8", "Closeout Report" });
                }
            }

           
            
            if (!ScaleOnlyMode)
            {
                dtbl.Rows.Add(new object[] { "85", "9", "Appointment Report" });
                dtbl.Rows.Add(new object[] { "82", "9", "House Account Report (Summary)" });
                dtbl.Rows.Add(new object[] { "81", "9", "House Account Statement" });
                dtbl.Rows.Add(new object[] { "84", "9", "Layaway Report" });
                dtbl.Rows.Add(new object[] { "80", "9", "Miscellaneous Lists" });
                dtbl.Rows.Add(new object[] { "92", "9", "Paid In/Out Report" });
                dtbl.Rows.Add(new object[] { "83", "9", "Store Credit Report" });
                dtbl.Rows.Add(new object[] { "88", "9", "Transaction Journal Detailed Report" });
                if (Settings.ActiveAdminPswdForMercury)
                {
                    dtbl.Rows.Add(new object[] { "90", "9", "View Logs" });
                }
            }
            else
            {
                dtbl.Rows.Add(new object[] { "80", "9", "Miscellaneous Lists" });
            }

            byte[] strip = GeneralFunctions.GetReportImageAsByteArray();

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

            grdRep.ItemsSource = dtblTemp;
            int i = 0;
            while (i < grdRep.VisibleRowCount)
            {
                int rH = grdRep.GetRowHandleByVisibleIndex(i);
                if (grdRep.IsGroupRowHandle(rH))
                {
                    grdRep.ExpandGroupRow(rH);
                    break;
                }
                i++;
            }



        }


        private void BuildTree()
        {
            bool ScaleOnlyMode = false;
            string RegisterModule = GeneralFunctions.RegisteredModules();
            if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
            {
                ScaleOnlyMode = true;
            }


            TreeListNode rootNode = null;
            if (!ScaleOnlyMode)
            {
                rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 0, ReportName = "Customer" });
                rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "customer_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 1, ReportGroupID = 0, ReportName = "Detail Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 2, ReportGroupID = 0, ReportName = "General Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 3, ReportGroupID = 0, ReportName = "Sales by Product Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 4, ReportGroupID = 0, ReportName = "Other Report ( Sales,Special Events etc )" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 5, ReportGroupID = 0, ReportName = "Repair List" });

                rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 1, ReportName = "CRM" });
                rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "time2_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 71, ReportGroupID = 1, ReportName = "Visit" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 72, ReportGroupID = 1, ReportName = "Points" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 73, ReportGroupID = 1, ReportName = "Parameters" });
            }

            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 3, ReportName = "Vendor" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "transit_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 11, ReportGroupID = 3, ReportName = "Detail Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 12, ReportGroupID = 3, ReportName = "General Report" });

            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 4, ReportName = "Product & Inventory" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "boproductgroup_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            if (!ScaleOnlyMode)
            {
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 21, ReportGroupID = 4, ReportName = "Detail Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 22, ReportGroupID = 4, ReportName = "General Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 23, ReportGroupID = 4, ReportName = "Kit Product Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 24, ReportGroupID = 4, ReportName = "Matrix Product Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 25, ReportGroupID = 4, ReportName = "Serialized Product Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 26, ReportGroupID = 4, ReportName = "Product Tax, Price Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 203, ReportGroupID = 4, ReportName = "Product Family Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 27, ReportGroupID = 4, ReportName = "Stock Journal Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 28, ReportGroupID = 4, ReportName = "Inventory Aging Report" });
            }

            if (!ScaleOnlyMode)
            {
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 215, ReportGroupID = 4, ReportName = "Stock Valuation Report" });

            }

            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 5, ReportName = "Employee" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "employee_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 31, ReportGroupID = 5, ReportName = "Detail Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 33, ReportGroupID = 5, ReportName = "Attendance Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 34, ReportGroupID = 5, ReportName = "Late Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 35, ReportGroupID = 5, ReportName = "Absent Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 36, ReportGroupID = 5, ReportName = "Payroll Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 38, ReportGroupID = 5, ReportName = "Payroll Export" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 37, ReportGroupID = 5, ReportName = "Special Events Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 39, ReportGroupID = 5, ReportName = "Employee Wage To Sales" });


            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 6, ReportName = "Ordering" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "financial_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 41, ReportGroupID = 6, ReportName = "Purchase Order Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 42, ReportGroupID = 6, ReportName = "Receiving Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 43, ReportGroupID = 6, ReportName = "Reorder Report" });
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 44, ReportGroupID = 6, ReportName = "Vendor Minimum Order Report" });

            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 7, ReportName = "Sales" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "bosale_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            if (!ScaleOnlyMode)
            {
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 51, ReportGroupID = 7, ReportName = "Sales Matrix Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 501, ReportGroupID = 7, ReportName = "Sales by Period" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 52, ReportGroupID = 7, ReportName = "Sales Summary Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 509, ReportGroupID = 7, ReportName = "Dashboard - Sales" });
                //CreateChildNode(rootNode, new ProjectObject() { ReportID = 59, ReportGroupID = 7, ReportName = "Matrix Item Sales Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 550, ReportGroupID = 7, ReportName = "Gift Aid Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 53, ReportGroupID = 7, ReportName = "Gift Cert. Balance Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 54, ReportGroupID = 7, ReportName = "Gift Cert. Activity Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 55, ReportGroupID = 7, ReportName = "Discount Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 56, ReportGroupID = 7, ReportName = "Card Transaction Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 58, ReportGroupID = 7, ReportName = "Packing List" });
            }

            CreateChildNode(rootNode, new ProjectObject() { ReportID = 60, ReportGroupID = 7, ReportName = "Sale Price List" });
            if (!ScaleOnlyMode)
            {
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 57, ReportGroupID = 7, ReportName = "Suspended Order List" });

                if (Settings.RegPOSAccess == "Y")
                {
                    rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 8, ReportName = "Closeout" });
                    rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "costanalysis_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
                    CreateChildNode(rootNode, new ProjectObject() { ReportID = 61, ReportGroupID = 8, ReportName = "Closeout Report" });
                }
            }

            rootNode = CreateRootNode(new ProjectObject() { ReportID = 0, ReportGroupID = 9, ReportName = "Miscellaneous" });
            rootNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "chartsshowlegend_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            CreateChildNode(rootNode, new ProjectObject() { ReportID = 80, ReportGroupID = 9, ReportName = "Miscellaneous Lists" });
            if (!ScaleOnlyMode)
            {
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 81, ReportGroupID = 9, ReportName = "House Account Statement" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 82, ReportGroupID = 9, ReportName = "House Account Report (Summary)" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 83, ReportGroupID = 9, ReportName = "Store Credit Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 84, ReportGroupID = 9, ReportName = "Layaway Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 85, ReportGroupID = 9, ReportName = "Appointment Report" });
                CreateChildNode(rootNode, new ProjectObject() { ReportID = 88, ReportGroupID = 9, ReportName = "Transaction Journal Detailed Report" });
                if (Settings.ActiveAdminPswdForMercury)
                {
                    CreateChildNode(rootNode, new ProjectObject() { ReportID = 90, ReportGroupID = 9, ReportName = "View Logs" });
                }

            }
        }

        private TreeListNode CreateRootNode(object dataObject)
        {
            TreeListNode rootNode = new TreeListNode(dataObject);
            //repview.Nodes.Add(rootNode);
            return rootNode;
        }

        private TreeListNode CreateChildNode(TreeListNode parentNode, object dataObject)
        {
            TreeListNode childNode = new TreeListNode(dataObject);
            //childNode.Image = new DXImageExtension() { Image = new DXImageConverter().ConvertFrom(null, null, "boreport_32x32.png") as DXImageInfo }.ProvideValue(null) as ImageSource;
            parentNode.Nodes.Add(childNode);
            return childNode;
        }

        public class ProjectObject
        {
            public int ReportID { get; set; }
            public int ReportGroupID { get; set; }
            public string ReportName { get; set; }
        }

        private void Repview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            PrintReport();
        }

        private void Repview_CustomRowAppearance(object sender, CustomRowAppearanceEventArgs e)
        {
            
        }

        private void Posscroll_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }


        private void Repview_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int intRowID = -1;
            //intRowID = GeneralFunctions.fnInt32((reptreelist.GetSelectedNodes()[0].Content as ProjectObject).ReportID.ToString());
            if (intRowID == 0)
            {
                btnPrint.IsEnabled = false;
                //btnSchd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                return;
            }
            else
            {
                if ((intRowID == 51) || (intRowID == 52) || (intRowID == 509))
                {
                    //btnSchd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    //btnSchd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                btnPrint.IsEnabled = true;
            }

            
        }

        public async Task PrintReport()
        {
            int intRowID = -1;
            intRowID = await ReturnRowID();
            if (intRowID == 0)
            {
                return;
            }

            #region Access Permission

            if ((!SecurityPermission.AcssCustomerDtlRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 1))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssCustomerGenRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 2))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssVendorDtlRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 11))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssVendorGenRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 12))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssProductDtlRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 21))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssProductGenRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 22))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssProductKitRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 23))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssProductMatxRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 24))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssEmployeeDtlRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 31))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssEmployeeAttnRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 33))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssEmployeeLateRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 34))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssEmployeeAbsentRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 35))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssOrderingPORep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 41))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssOrderingRecvRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 42))
            {
                DocMessage.MsgPermission();
                return;
            }
            if ((!SecurityPermission.AcssOrderingReorderRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 43))
            {
                DocMessage.MsgPermission();
                return;
            }

            if ((!SecurityPermission.AcssCustomerOtherRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 4))
            {
                DocMessage.MsgPermission();
                return;
            }

            if ((!SecurityPermission.AcssSalesRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 51))
            {
                DocMessage.MsgPermission();
                return;
            }

            if ((!SecurityPermission.AcssSalesSummaryRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 52))
            {
                DocMessage.MsgPermission();
                return;
            }

            if ((!SecurityPermission.AcssSalesCardTranRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 56))
            {
                DocMessage.MsgPermission();
                return;
            }

            if ((!SecurityPermission.AcssMinimumOrderRep) && (SystemVariables.CurrentUserID > 0) && (intRowID == 44))
            {
                DocMessage.MsgPermission();
                return;
            }

            #endregion


            //  Customer

            if (intRowID == 1) OpenLookupReportDlg("Customer");                 // Detail Report
            if (intRowID == 2) OpenCustomerReportDlg();                         // General Report
            if (intRowID == 3) OpenCustomerSalesByProductReport();              // Sales by Product Report      
            if (intRowID == 4) OpenCustomerOtherReportDlg();                    // Other Report ( Sales,Special Events etc )
            if (intRowID == 5) OpenCustomerRepairs();                           // Repair List

            //  Vendor

            if (intRowID == 11) OpenLookupReportDlg("Vendor");                  // Detail Report
            if (intRowID == 12) OpenVendorReportDlg();                          // General Report

            // Product

            if (intRowID == 21) OpenLookupReportDlg("Product");                 // Detail Report
            if (intRowID == 22) OpenProductReportDlg();                         // General Report
            if (intRowID == 23) OpenKitReportDlg();                             // Kit Product Report
            if (intRowID == 24) OpenMatrixReportDlg();                          // Matrix Product Report
            if (intRowID == 25) OpenSerializedProductReportDlg();               // Serialized Product Report
            if (intRowID == 26) OpenProductTaxPriceReport();                    // Product Tax, Price Report
            if (intRowID == 203) OpenProductFamilyReport();                     // Product Family Report
            if (intRowID == 27) OpenInvJournalReportDlg();                      // Stock Journal Report
            if (intRowID == 28) OpenAgingReport();                              // Inventory Aging Report
            if (intRowID == 29) OpenScaleReport("G");                           // Scale Report
            if (intRowID == 30) OpenScaleReport("S");                           // Scale Report - Nutritional Data, Ingredient etc.
            if (intRowID == 201) OpenScalePriceChangeExceptionDlg();            // Scale Price Change Exception
            if (intRowID == 202) OpenEmbeddedInventoryDlg();                    // Random Weight Inventory
            if (intRowID == 204) OpenMarkdownReport();                          // Markdown
            if (intRowID == 215) ExecuteInventoryValuationReport();                          // Stock Valuation

            if (intRowID == 209) ExecuteProductExpiryReport();                          

            // Employee

            if (intRowID == 31) OpenLookupReportDlg("Employee");                // Detail Report
            if (intRowID == 33) OpenAttendanceReportDlg();                      // Attendance Report
            if (intRowID == 34) OpenLateReportDlg();                            // Late Report
            if (intRowID == 35) OpenAbsentReportDlg();                          // Absent Report
            if (intRowID == 36) OpenPayrollReportDlg("N");                      // Payroll Report
            if (intRowID == 38) OpenPayrollReportDlg("Y");                      // Payroll Export
            if (intRowID == 37) OpenEmpOtherReportDlg();                        // Special Events Report
            if (intRowID == 39) OpenEmployeeWageToSalesReport();                // Employee Wage To Sales

            // Ordering
            if (intRowID == 41) OpenOrderingReportDlg("Purchase Order");        // Purchase Order Report
            if (intRowID == 42) OpenOrderingReportDlg("Receiving Items");       // Receiving Report
            if (intRowID == 43) OpenReorderReportDlg();                         // Reorder Report
            if (intRowID == 44) OpenMinimumOrderReport();                       // Vendor Minimum Order Report

            // Sales
            if (intRowID == 509) OpenDashboardReport();
            if (intRowID == 51) OpenSalesReportDlg();                           // Sales Matrix Report
            if (intRowID == 501) OpenSalesByPeriodReportDlg();                   // Sales By Period
            if (intRowID == 52) OpenSalesSummaryReportDlg();                    // Sales Summary Report
            if (intRowID == 59) OpenMatrixItemSalesReport();                    // Matrix Item Sales Report
            if (intRowID == 53) OpenGCBalReportDlg();                           // Gift Cert. Balance Report
            if (intRowID == 54) OpenGCRedeemReportDlg();                        // Gift Cert. Activity Report
            if (intRowID == 55) OpenDiscountList();                             // Discount Report
            if (intRowID == 550) OpenGiftAidReport();                            // Discount Report
            if (intRowID == 56) OpenCardTransactionReport();                    // Card Transaction Report
            if (intRowID == 58) OpenPackingList();                              // Packing List
            if (intRowID == 57) OpenSuspendedOrderReport();                     // Suspended Order List
            if (intRowID == 60) OpenSalePriceList();                            // Sale Price List
            if (intRowID == 511) OpenTaxAnalysisReport();

            // Closeout

            if (intRowID == 61) OpenCloseoutReportDlg();                        // Closeout Report

            // CRM
            if (intRowID == 71) OpenCRM_VisitReportDlg();                       // Visit            
            if (intRowID == 72) OpenCRM_PointReportDlg();                       // Point
            if (intRowID == 73) OpenCRM_ParamReportDlg();                       // Param

            // Miscellaneous

            if (intRowID == 80) OpenListDlg();                                  // Miscellaneous Lists
            if (intRowID == 81) OpenHAStatementReport();                        // House Account Statement
            if (intRowID == 82) OpenGenaralReport(1);                           // House Account Report (Summary)
            if (intRowID == 83) OpenGenaralReport(2);                           // Store Credit Report
            if (intRowID == 84) OpenLayawayReportDlg();                         // Layaway Report
            if (intRowID == 85) OpenApptReport();                               // Appointment Report
            if (intRowID == 88) OpenTransactionJournalReport();                 // Transaction Journal
            if (intRowID == 90)                                                 // View Logs
            {
                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (SystemVariables.CurrentUserID == 0)
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event11, "Successful", "");
                    else
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event11, "Successful", "");
                }
                OpenLogs();
            }

            if (intRowID == 92) OpenPaidInOutReport();


        }


        #region All Report Dialogs

        private void ExecuteProductExpiryReport()
        {
            frm_ProductExpiryReportDlg frm = new Report.frm_ProductExpiryReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
            }
        }

        private void OpenPaidInOutReport()
        {
            frm_PaidInOutReportDlg frm = new Report.frm_PaidInOutReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
            }
        }

        private void OpenMarkdownReport()
        {
            Report.frm_MarkdownReportDlg frm = new Report.frm_MarkdownReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
            }
        }

        private void OpenTaxAnalysisReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_TaxAnalysisReportDlg frm = new Report.frm_TaxAnalysisReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenDashboardReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_DashboardReportDlg frm = new Report.frm_DashboardReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenEmbeddedInventoryDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_RandomWeightInventoryReport frm = new Report.frm_RandomWeightInventoryReport();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenScalePriceChangeExceptionDlg()
        {
            Report.frm_ScalePriceChangeReportDlg frm = new Report.frm_ScalePriceChangeReportDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
            }
        }

        private void OpenListDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ListDlg frm = new Report.frm_ListDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSalesByPeriodReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SalesByPeriod frm = new Report.frm_SalesByPeriod();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCustomerReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CustomerReportDlg frm_CustomerReportDlg = new frm_CustomerReportDlg();
            try
            {
                frm_CustomerReportDlg.ShowDialog();
            }
            finally
            {
                frm_CustomerReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenVendorReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_VendorReportDlg frm_VendorReportDlg = new frm_VendorReportDlg();
            try
            {
                frm_VendorReportDlg.ShowDialog();
            }
            finally
            {
                frm_VendorReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenProductReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ProductReportDlg frm_ProductReportDlg = new Report.frm_ProductReportDlg();
            try
            {
                frm_ProductReportDlg.ShowDialog();
            }
            finally
            {
                frm_ProductReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenAttendanceReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpAttendanceReportDlg frm_EmpAttendanceReportDlg = new frm_EmpAttendanceReportDlg();
            try
            {
                frm_EmpAttendanceReportDlg.ReportID = 2;
                frm_EmpAttendanceReportDlg.ShowDialog();
            }
            finally
            {
                frm_EmpAttendanceReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenLateReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpAttendanceReportDlg frm_EmpAttendanceReportDlg = new frm_EmpAttendanceReportDlg();
            try
            {
                frm_EmpAttendanceReportDlg.ReportID = 1;
                frm_EmpAttendanceReportDlg.ShowDialog();
            }
            finally
            {
                frm_EmpAttendanceReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenAbsentReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpAttendanceReportDlg frm_EmpAttendanceReportDlg = new frm_EmpAttendanceReportDlg();
            try
            {
                frm_EmpAttendanceReportDlg.ReportID = 3;
                frm_EmpAttendanceReportDlg.ShowDialog();
            }
            finally
            {
                frm_EmpAttendanceReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenKitReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_KitReportDlg frm_KitReportDlg = new Report.frm_KitReportDlg();
            try
            {
                frm_KitReportDlg.ShowDialog();
            }
            finally
            {
                frm_KitReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenMatrixReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_MatrixReportDlg frm_MatrixReportDlg = new Report.frm_MatrixReportDlg();
            try
            {
                frm_MatrixReportDlg.ShowDialog();
            }
            finally
            {
                frm_MatrixReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenLookupReportDlg(string strReportType)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_Lookups frm_Lookups = new Report.frm_Lookups();
            try
            {
                frm_Lookups.Print = "Y";
                frm_Lookups.SearchType = strReportType;
                frm_Lookups.ShowDialog();
            }
            finally
            {
                frm_Lookups.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenOrderingReportDlg(string strReportType)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_OrderLookUpDlg frm_OrderLookUpDlg = new Report.frm_OrderLookUpDlg();
            try
            {
                frm_OrderLookUpDlg.Print = "Y";
                frm_OrderLookUpDlg.SearchType = strReportType;
                frm_OrderLookUpDlg.ShowDialog();
            }
            finally
            {
                frm_OrderLookUpDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenReorderReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ReorderReportDlg frm_ReorderReportDlg = new Report.frm_ReorderReportDlg();
            try
            {
                frm_ReorderReportDlg.ShowDialog();
            }
            finally
            {
                frm_ReorderReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenPayrollReportDlg(string expf)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpPayrollReportDlg frm_EmpPayrollReportDlg = new Report.frm_EmpPayrollReportDlg();
            try
            {
                frm_EmpPayrollReportDlg.ExportFlag = expf;
                frm_EmpPayrollReportDlg.ShowDialog();
            }
            finally
            {
                frm_EmpPayrollReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSalesReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SalesReportDlg frm_SalesReportDlg = new Report.frm_SalesReportDlg();
            try
            {
                frm_SalesReportDlg.ShowDialog();
            }
            finally
            {
                frm_SalesReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSalesSummaryReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SalesSummaryReportDlg frm_SalesSummaryReportDlg = new Report.frm_SalesSummaryReportDlg();
            try
            {
                frm_SalesSummaryReportDlg.ShowDialog();
            }
            finally
            {
                frm_SalesSummaryReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCustomerOtherReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CustomerOtherReportDlg frm_CustomerOtherReportDlg = new Report.frm_CustomerOtherReportDlg();
            try
            {
                frm_CustomerOtherReportDlg.ShowDialog();
            }
            finally
            {
                frm_CustomerOtherReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCloseoutReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            UserControls.frm_CloseoutReprintDlg frm_CloseoutReprintDlg = new UserControls.frm_CloseoutReprintDlg();
            try
            {
                frm_CloseoutReprintDlg.CalledFor = "Report";
                frm_CloseoutReprintDlg.ShowDialog();
            }
            finally
            {
                frm_CloseoutReprintDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenInvJournalReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_JournalReportDlg frm_JournalReportDlg = new Report.frm_JournalReportDlg();
            try
            {
                frm_JournalReportDlg.ShowDialog();
            }
            finally
            {
                frm_JournalReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCRM_VisitReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CRMVisitDlg frm_CRMVisitDlg = new Report.frm_CRMVisitDlg();
            try
            {
                frm_CRMVisitDlg.ShowDialog();
            }
            finally
            {
                frm_CRMVisitDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCRM_ParamReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CRMParamDlg frm_CRMParamDlg = new Report.frm_CRMParamDlg();
            try
            {
                frm_CRMParamDlg.ShowDialog();
            }
            finally
            {
                frm_CRMParamDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCRM_PointReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CRMPointsDlg frm_CRMPointsDlg = new Report.frm_CRMPointsDlg();
            try
            {
                frm_CRMPointsDlg.ShowDialog();
            }
            finally
            {
                frm_CRMPointsDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenGCBalReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_GCBalReportDlg frm_GCBalReportDlg = new Report.frm_GCBalReportDlg();
            try
            {
                frm_GCBalReportDlg.ShowDialog();
            }
            finally
            {
                frm_GCBalReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenGCRedeemReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_GCReportDlg frm_GCReportDlg = new Report.frm_GCReportDlg();
            try
            {
                frm_GCReportDlg.ShowDialog();
            }
            finally
            {
                frm_GCReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSerializedProductReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SerializedProductReportDlg frm_SerializedProductReportDlg = new Report.frm_SerializedProductReportDlg();
            try
            {
                frm_SerializedProductReportDlg.ShowDialog();
            }
            finally
            {
                frm_SerializedProductReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenLayawayReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_LayawayReportDlg frm_LayawayReportDlg = new Report.frm_LayawayReportDlg();
            try
            {
                frm_LayawayReportDlg.ShowDialog();
            }
            finally
            {
                frm_LayawayReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenEmpOtherReportDlg()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpOtherReportDlg frm_EmpOtherReportDlg = new Report.frm_EmpOtherReportDlg();
            try
            {
                frm_EmpOtherReportDlg.ShowDialog();
            }
            finally
            {
                frm_EmpOtherReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenHAStatementReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_HAStatementDlg frm_HAStatementDlg = new Report.frm_HAStatementDlg();
            try
            {
                frm_HAStatementDlg.ShowDialog();
            }
            finally
            {
                frm_HAStatementDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCardTransactionReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CardTranRepDlg frm_CardTranRepDlg = new Report.frm_CardTranRepDlg();
            try
            {
                frm_CardTranRepDlg.ShowDialog();
            }
            finally
            {
                frm_CardTranRepDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenProductTaxPriceReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ProductTaxPriceReportDlg frm_ProductTaxPriceReportDlg = new Report.frm_ProductTaxPriceReportDlg();
            try
            {
                frm_ProductTaxPriceReportDlg.ShowDialog();
            }
            finally
            {
                frm_ProductTaxPriceReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenPackingList()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_PackinglistDlg frm_PackinglistDlg = new Report.frm_PackinglistDlg();
            try
            {
                frm_PackinglistDlg.FromPOS = false;
                frm_PackinglistDlg.ShowDialog();
            }
            finally
            {
                frm_PackinglistDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenGiftAidReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_repGiftAid frm_repDiscount = new Report.frm_repGiftAid();
            try
            {
                frm_repDiscount.ShowDialog();
            }
            finally
            {
                frm_repDiscount.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenDiscountList()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_repDiscount frm_repDiscount = new Report.frm_repDiscount();
            try
            {
                frm_repDiscount.ShowDialog();
            }
            finally
            {
                frm_repDiscount.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenMinimumOrderReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_MinimumOrderReport frm_MinimumOrderReport = new Report.frm_MinimumOrderReport();
            try
            {
                frm_MinimumOrderReport.ShowDialog();
            }
            finally
            {
                frm_MinimumOrderReport.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCustomerSalesByProductReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CustSalesByProductReport frm_CustSalesByProductReport = new Report.frm_CustSalesByProductReport();
            try
            {
                frm_CustSalesByProductReport.ShowDialog();
            }
            finally
            {
                frm_CustSalesByProductReport.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenLogs()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_Logs frm_Logs = new Report.frm_Logs();
            try
            {
                frm_Logs.ShowDialog();
            }
            finally
            {
                frm_Logs.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenApptReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ApptRepDlg frm_appt = new Report.frm_ApptRepDlg();
            try
            {
                frm_appt.ShowDialog();
            }
            finally
            {
                frm_appt.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenAgingReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_AgingRepDlg frmrep = new Report.frm_AgingRepDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenTransactionJournalReport()
        {
            /*(Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_TranJournalDlg frmrep = new Report.frm_TranJournalDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }*/

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_TrnJrnDReportDlg frm_TableSalesReportDlg = new frm_TrnJrnDReportDlg();
            try
            {
                frm_TableSalesReportDlg.ShowDialog();
            }
            finally
            {
                frm_TableSalesReportDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenMatrixItemSalesReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_MatrixSalesReportDlg frmrep = new Report.frm_MatrixSalesReportDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenScaleReport(string prm)
        {
            /*Report.frm_ScaleReportDlg frmrep = new Report.frm_ScaleReportDlg();
            try
            {
                frmrep.FormCategory = prm;
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
            }*/
        }

        private void OpenEmployeeWageToSalesReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_EmpWageToSalesReportDlg frmrep = new Report.frm_EmpWageToSalesReportDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenProductFamilyReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ProductFamilyReportDlg frmrep = new Report.frm_ProductFamilyReportDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenGenaralReport(int RID)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_GeneralReportDlg frmrep = new Report.frm_GeneralReportDlg();
            try
            {
                frmrep.ID = RID;
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenCustomerRepairs()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_CustomerRepairReport frmrep = new Report.frm_CustomerRepairReport();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSuspendedOrderReport()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SuspendReportDlg frmrep = new Report.frm_SuspendReportDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenSalePriceList()
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_SalesPriceReportDlg frmrep = new Report.frm_SalesPriceReportDlg();
            try
            {
                frmrep.ShowDialog();
            }
            finally
            {
                frmrep.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        private void ExecuteInventoryValuationReport()
        {
            try
            {
                Cursor = Cursors.Wait;
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.Product objSales = new PosDataObject.Product();
                objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objSales.FetchInventoryValuationReportData();

                foreach(DataRow dr in dtbl.Rows)
                {
                    DataTable dVend = objSales.GetSupplierForInventoryValuationReport(GeneralFunctions.fnInt32(dr["ID"].ToString()));
                    foreach (DataRow dr1 in dVend.Rows)
                    {
                        dr["Vendor"] = dr1["Vendor"].ToString();
                        dr["Part"] = dr1["Part"].ToString();
                    }
                    dVend.Dispose();
                }

                OfflineRetailV2.Report.Product.repInventoryValuation rep_StoreCredit = new OfflineRetailV2.Report.Product.repInventoryValuation();


                rep_StoreCredit.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_StoreCredit);
                rep_StoreCredit.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_StoreCredit.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_StoreCredit.rRange.Text = "As on " + DateTime.Now.ToString(SystemVariables.DateFormat + " hh:mm tt");
                rep_StoreCredit.DecimalPlace = Settings.DecimalPlace;

                if (Settings.DecimalPlace == 3) rep_StoreCredit.rTQty.Summary.FormatString = "{0:0.000}";
                else rep_StoreCredit.rTQty.Summary.FormatString = "{0:0.00}";

                if (Settings.DecimalPlace == 3) rep_StoreCredit.rTValue.Summary.FormatString = "{0:0.000}";
                else rep_StoreCredit.rTValue.Summary.FormatString = "{0:0.00}";

                rep_StoreCredit.rSKU.DataBindings.Add("Text", dtbl, "SKU");
                rep_StoreCredit.rDesc.DataBindings.Add("Text", dtbl, "Description");
                rep_StoreCredit.rCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_StoreCredit.rDCost.DataBindings.Add("Text", dtbl, "DCost");
                rep_StoreCredit.rQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
                rep_StoreCredit.rValue.DataBindings.Add("Text", dtbl, "StockValue");
                rep_StoreCredit.rTQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
                rep_StoreCredit.rTValue.DataBindings.Add("Text", dtbl, "StockValue");
                rep_StoreCredit.rAltSKU.DataBindings.Add("Text", dtbl, "AltSKU");
                rep_StoreCredit.rCategory.DataBindings.Add("Text", dtbl, "Category");
                rep_StoreCredit.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
                rep_StoreCredit.rPart.DataBindings.Add("Text", dtbl, "Part");
                rep_StoreCredit.rPriceA.DataBindings.Add("Text", dtbl, "SalePrice");

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_StoreCredit.PrinterName = Settings.ReportPrinterName;
                    rep_StoreCredit.CreateDocument();
                    rep_StoreCredit.PrintingSystem.ShowMarginsWarning = false;
                    rep_StoreCredit.PrintingSystem.ShowPrintStatusDialog = false;
                    rep_StoreCredit.ShowPreviewDialog();

                }
                finally
                {
                    rep_StoreCredit.Dispose();

                    dtbl.Dispose();
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            await PrintReport();
        }

        private void Repview_CustomNodeFilter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Node.IsChecked = false;
                
            }
        }

        private void BtnSchd_Click(object sender, RoutedEventArgs e)
        {
            frm_ReportSchedule frm = new frm_ReportSchedule();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
            }
        }

        private async void GrdRep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await PrintReport();
        }

        private void GrdRep_CustomGroupDisplayText(object sender, CustomGroupDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colGroup")
            {
                if (e.Value.ToString() == "0")
                {
                    e.DisplayText = "Customer";
                }
                if (e.Value.ToString() == "1")
                {
                    e.DisplayText = "CRM";
                }
                if (e.Value.ToString() == "3")
                {
                    e.DisplayText = "Vendor";
                }
                if (e.Value.ToString() == "4")
                {
                    e.DisplayText = "Product & Inventory";
                }
                if (e.Value.ToString() == "5")
                {
                    e.DisplayText = "Employee";
                }
                if (e.Value.ToString() == "6")
                {
                    e.DisplayText = "Ordering";
                }
                if (e.Value.ToString() == "7")
                {
                    e.DisplayText = "Sales";
                }
                if (e.Value.ToString() == "8")
                {
                    e.DisplayText = "Closeout";
                }
                if (e.Value.ToString() == "9")
                {
                    e.DisplayText = "Miscellaneous";
                }
            }
        }
    }
}
