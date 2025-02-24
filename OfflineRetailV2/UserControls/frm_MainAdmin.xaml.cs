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
using Microsoft.Win32;
using Microsoft.PointOfService;
using DevExpress.Mvvm.UI;
using System.Windows.Threading;
using DevExpress.Xpf.NavBar;
using OfflineRetailV2.ProductsLabel;
using System.Net;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_MainAdmin.xaml
    /// </summary>
    public partial class frm_MainAdmin : UserControl
    {
        private bool blSetNavBar = false;
        private int intSetNavBarCustomer = 0;
        private int intSetNavBarProduct = 0;
        private int intSetNavBarOrder = 0;
        private int intSetNavBarEmployee = 0;
        private int intSetNavBarSetup = 0;
        private int intSetNavBarOther = 0;
        private int intSetNavBarLabel = 0;
        private bool blFunctionBtnAccess = false;
        private bool blFunctionOrderChangeAccess = false;

        private DockPanel SelectedDocPanel = null;


        UserControls.POSSection.frmZipCodeBrwUC frmZipCodeBrwUC = null;
        UserControls.POSSection.frm_CustomerBrwUC frm_CustomerBrwUC = null;
        UserControls.POSSection.frm_BrandBrwUC frm_BrandBrwUC = null;
        UserControls.POSSection.frm_ClassBrwUC frm_ClassBrwUC = null;
        UserControls.POSSection.frm_GroupBrwUC frm_GroupBrwUC = null;
        UserControls.POSSection.frmProductBrwUC frmProductBrwUC = null;
        UserControls.POSSection.frm_ServiceBrwUC frm_ServiceBrwUC = null;
        UserControls.POSSection.frm_CategoryBrwUC frm_CategoryBrwUC = null;
        UserControls.POSSection.frm_DepartmentBrwUC frm_DepartmentBrwUC = null;
        UserControls.POSSection.frm_JournalBrwUC frm_JournalBrwUC = null;
        UserControls.POSSection.frm_StocktakeBrwUC frm_StocktakeBrwUC = null;
        UserControls.POSSection.frm_SalePriceBrwUC frm_SalePriceBrwUC = null;
        UserControls.POSSection.frm_FuturePriceBrwUC frm_FuturePriceBrwUC = null;
        UserControls.POSSection.frm_POBrwUC frm_POBrwUC = null;
        UserControls.frm_VendorBrwUC frm_VendorBrwUC = null;
        UserControls.frm_ReceivingBrwUC frm_ReceivingBrwUC = null;
        UserControls.frm_TransferBrwUC frm_TransferBrwUC = null;
        UserControls.frm_DiscountBrwUC frm_DiscountBrwUC = null;
        UserControls.frm_BuynGetFreeBrwUC frm_BuynGetFreeBrwUC = null;

        UserControls.frm_Mix_n_MatchBrwUC frm_Mix_n_MatchBrwUC = null;
        UserControls.frm_BreakPackBrwUC frm_BreakPackBrwUC = null;
        UserControls.frm_EmployeeBrwUC frm_EmployeeBrwUC = null;
        UserControls.frmShiftBrwUC frmShiftBrwUC = null;
        UserControls.frmHolidayBrwUC frmHolidayBrwUC = null;


        UserControls.Administrator.frm_FeesBrwUC frm_FeesBrwUC = null;
        UserControls.Administrator.frm_TaxBrwUC frm_TaxBrwUC = null;
        UserControls.Administrator.frm_SecurityGroupBrwUC frm_SecurityGroupBrwUC = null;
        UserControls.Administrator.frm_GLBrwUC frm_GLBrwUC = null;
        UserControls.Administrator.frm_TenderTypesBrwUC frm_TenderTypesBrwUC = null;
        UserControls.Administrator.frm_ImportCustomerBrwUC frm_ImportCustomerBrwUC = null;
        UserControls.Administrator.frm_InventoryBrwUC frm_InventoryBrwUC = null;
        UserControls.Administrator.frm_GiftCertBrwUC frm_GiftCertBrwUC = null;
        UserControls.Administrator.frm_PrintLabelBrwUC frm_PrintLabelBrwUC = null;
        UserControls.Administrator.frm_ProductsLabelPrinting frm_ProductsLabelPrinting = null;
        UserControls.Administrator.frm_PrinterTemplateBrwUC frm_PrinterTemplateBrwUC = null;
        UserControls.Administrator.frm_PrinterTypeBrwUC frm_PrinterTypeBrwUC = null;
        

        public frm_MainAdmin()
        {
            InitializeComponent();

        }



        public CommandBase AdminCloseCommand { get; set; }


        private bool IsPOSInstalled()
        {
            bool blf = false;
            try
            {
                PosExplorer psexp = new PosExplorer();
                blf = true;
            }
            catch (Exception ex)
            {
                blf = false;
            }
            return blf;
        }

        private void HideAllBrowseForm(string VisibleControlName)
        {
            foreach (UserControl uc in pnlBody.Children)
            {
                if (uc.Name == VisibleControlName)
                {
                    uc.Visibility = Visibility.Visible;
                }
                else
                {
                    uc.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void HideAllBrowseForm()
        {
            foreach (UserControl uc in pnlBody.Children)
            {
                uc.Visibility = Visibility.Collapsed;
            }
        }



        private void NbDefinedParam_Click(object sender, EventArgs e)
        {

        }







        private void NbPriceAdjustment_Click(object sender, EventArgs e)
        {

        }

        private void NbDefinedParam_Click_1(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_ClientParamDlg frm_ = new POSSection.frm_ClientParamDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbRegistration_Click(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmRegistrationDlg frm_RegistrationDlg = new frmRegistrationDlg();
            try
            {
                frm_RegistrationDlg.FirstTimeCall = false;
                frm_RegistrationDlg.ShowDialog();
                if (frm_RegistrationDlg.DialogResult == true)
                {
                    if (frm_RegistrationDlg.Registered)
                    {
                        //StatusItem4.Caption = " " + Translation.SetMultilingualTextInCodes("Registered to :", "frmMain_Registeredto") + " " + Settings.RegCompanyName;
                        DocMessage.MsgInformation("Successful Registration, Terminating Application...");
                        Application.Current.Shutdown();
                    }
                }
            }
            finally
            {
                frm_RegistrationDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbPriceAdjustment_Click_1(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PriceAdjustmentDlg frm_ = new frm_PriceAdjustmentDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }



        // Check General Setup Access
        private void CheckAccessForGeneralSettings(string scode)
        {
            blFunctionBtnAccess = false;
            blFunctionOrderChangeAccess = false;
            if (SystemVariables.CurrentUserID <= 0)
            {
                blFunctionOrderChangeAccess = true;
                blFunctionBtnAccess = true;
                return;
            }
            else
            {
                PosDataObject.Security objSecurity = new PosDataObject.Security();
                objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int result = objSecurity.IsExistsPOSAccess(SystemVariables.CurrentUserID, scode);
                if (result == 0)
                {
                    blFunctionOrderChangeAccess = false;
                    blFunctionBtnAccess = true;
                    return;
                }
                else
                {
                    blFunctionOrderChangeAccess = true;
                    blFunctionBtnAccess = true;
                    return;
                }
            }
        }

        private void NbScaleConfig_Click(object sender, EventArgs e)
        {

            Administrator.frm_EmailTestDlg frm_ = new Administrator.frm_EmailTestDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
            }
        }

        private void NbGwareHostSetup_Click(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Administrator.frm_GwareHostSetupDlg frm_ = new Administrator.frm_GwareHostSetupDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }



        public void LoadAdmin()
        {

            (Window.GetWindow(this) as MainWindow).btnFrontOffice.PreviewMouseLeftButtonDown += BtnFrontOffice_PreviewMouseLeftButtonDown;
            pnlMenuHeader.Width = 140.0;
            imghide.Visibility = Visibility.Visible;
            imgshow.Visibility = Visibility.Collapsed;
            lbShowHideH.Text = "Hide";
            btnECHeader.SetValue(Canvas.LeftProperty, 80.0);
            //HideAllGroupItems();
            HideAllGroupItems1();
            //SetCustomNavBar();
            SetCustomNavBar1();

            if (SelectedDocPanel != null)
            {
                SetItemsPanelFromSelectedGroup(SelectedDocPanel);
            }




            /* if ((navGroupCustomer1.Visibility == Visibility.Collapsed) && (navGroupProducts1.Visibility == Visibility.Collapsed) && (navGroupOrder1.Visibility == Visibility.Collapsed)
                && (navGroupOrder1.Visibility == Visibility.Collapsed) && (navGroupReports1.Visibility == Visibility.Collapsed) && (navGroupEmp1.Visibility == Visibility.Collapsed)
                && (navGroupSetup1.Visibility == Visibility.Collapsed) && (navGroupDiscount1.Visibility == Visibility.Collapsed) && (navGroupCO1.Visibility == Visibility.Collapsed))
             {
                 HideAllBrowseForm();
                 pnlBody.Refresh();
                 pnlBody.UpdateLayout();
             } */


            if ((mnuCustomer.Visibility == Visibility.Collapsed) && (mnuItem.Visibility == Visibility.Collapsed) && (mnuOrdering.Visibility == Visibility.Collapsed)
              && (mnuReport.Visibility == Visibility.Collapsed) && (mnuEmployee.Visibility == Visibility.Collapsed)
               && (mnuSettings.Visibility == Visibility.Collapsed) && (mnuDiscounts.Visibility == Visibility.Collapsed) && (mnuHost.Visibility == Visibility.Collapsed))
            {
                HideAllBrowseForm();
                pnlBody.Refresh();
                pnlBody.UpdateLayout();
            }

            if (GeneralFunctions.GetRecordCount("Setup") == 0)
            {

                if (IsPOSInstalled())
                {

                    CheckAccessForGeneralSettings("31z1");
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                    Administrator.frm_GeneralSetupDlg frm_GeneralSetupDlg = new Administrator.frm_GeneralSetupDlg();
                    try
                    {
                        frm_GeneralSetupDlg.CallFromAdmin = true;
                        frm_GeneralSetupDlg.MainF = this;
                        frm_GeneralSetupDlg.FunctionOrderChangeAccess = blFunctionOrderChangeAccess;
                        frm_GeneralSetupDlg.FunctionBtnAccess = blFunctionBtnAccess;
                        frm_GeneralSetupDlg.ShowDialog();
                    }
                    finally
                    {

                        frm_GeneralSetupDlg.Close();
                        (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    DocMessage.MsgInformation("You must install POS for .Net");
                }
            }

            if (Settings.DefautInternational == "Y")
            {

                CheckAccessForGeneralSettings("31z1");
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_GeneralSetupDlg frm_GeneralSetupDlg = new Administrator.frm_GeneralSetupDlg();
                try
                {
                    frm_GeneralSetupDlg.CallFromAdmin = true;
                    frm_GeneralSetupDlg.MainF = this;
                    frm_GeneralSetupDlg.FunctionOrderChangeAccess = blFunctionOrderChangeAccess;
                    frm_GeneralSetupDlg.FunctionBtnAccess = blFunctionBtnAccess;
                    frm_GeneralSetupDlg.tcSetup.SelectedIndex = 9;
                    frm_GeneralSetupDlg.ShowDialog();
                }
                finally
                {

                    frm_GeneralSetupDlg.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (Settings.DefautInternational == "Y")
            {
                Application.Current.Shutdown();
            }

        }

        private void BtnFrontOffice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
            (Window.GetWindow(this) as MainWindow).UpdateLayout();
            (Window.GetWindow(this) as MainWindow).GoToFrontOffice();
        }

        private void NbReorder_Click(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ReorderReportDlg frm_ = new Report.frm_ReorderReportDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }



        private void Grp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool boolProccedChanged = true;
            DockPanel dp = sender as DockPanel;
            foreach (UIElement uI in MenuPanel.Children)
            {
                if ((uI as DockPanel).Tag.ToString() == "99") continue; // Do not consider Exit
                if ((uI as DockPanel).Tag.ToString() == "1")
                {
                    if ((uI as DockPanel).Name == dp.Name)
                    {
                        boolProccedChanged = false;
                        break;
                    }
                }
            }

            if (boolProccedChanged)
            {
                SetItemsPanelFromSelectedGroup(dp);
            }

        }



        private void HideAllGroupItems1()
        {

            imgMenu_C.Source = this.FindResource("CustomerMenu") as ImageSource;
            imgMenu_P.Source = this.FindResource("ItemMenu") as ImageSource;
            imgMenu_O.Source = this.FindResource("OrderingMenu") as ImageSource;
            imgMenu_R.Source = this.FindResource("ReportMenu") as ImageSource;
            imgMenu_E.Source = this.FindResource("EmployeeMenu") as ImageSource;
            imgMenu_S.Source = this.FindResource("SettingsMenu") as ImageSource;
            imgMenu_D.Source = this.FindResource("DiscountMenu") as ImageSource;
            imgMenu_H.Source = this.FindResource("HostMenu") as ImageSource;
        }

        private void SetItemsPanelFromSelectedGroup(DockPanel dp)
        {
            HideAllGroupItems1();

            foreach (UIElement uI in MenuPanel.Children)
            {
                //if ((uI as DockPanel).Tag.ToString() == "99") continue;
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));

                    if ((uI as DockPanel).Name == "mnuCustomer") imgMenu_C.Source = this.FindResource("CustomerMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuItem") imgMenu_P.Source = this.FindResource("ItemMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuOrdering") imgMenu_O.Source = this.FindResource("OrderingMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuReport") imgMenu_R.Source = this.FindResource("ReportMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuEmployee") imgMenu_E.Source = this.FindResource("EmployeeMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuSettings") imgMenu_S.Source = this.FindResource("SettingsMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuDiscounts") imgMenu_D.Source = this.FindResource("DiscountMenuS") as ImageSource;
                    if ((uI as DockPanel).Name == "mnuHost") imgMenu_H.Source = this.FindResource("HostMenuS") as ImageSource;
                    /*
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is TextBlock)
                        {
                            (ulsub as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                            lbselectedGrp.Text = (ulsub as TextBlock).Text;
                        }
                    }*/
                }
                else
                {
                    (uI as DockPanel).Tag = "0";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));

                    /*foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is TextBlock)
                        {
                            (ulsub as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                        }
                    }*/
                }
            }

            if (dp.Name == "mnuReport")
            {
                HideAllBrowseForm("frm_ReportsUC");
                frm_ReportsUC.LoadData();
                lbHeading.Text = "List of Reports";
            }
            else
            {
                InitiateClickOnGroupChange(dp);
            }
        }

        private void InitiateClickOnGroupChange(DockPanel wp)
        {
            //if (wp == mnuLabelPrint)
            //{
            //    frm_ProductsLabel sd = new frm_ProductsLabel();
            //    sd.ShowDialog();
            //}

            if (wp == mnuCustomer)
            {
                HideAllBrowseForm("frm_SMCustomerBrwUC");
                frm_SMCustomerBrwUC.ParentForm = this;
                frm_SMCustomerBrwUC.LoadSubmenu();

                /*
                if (((SecurityPermission.AccessCustomerScreen) && (intSetNavBarCustomer == 1)) || (SystemVariables.CurrentUserID <= 0))
                {

                    if (frm_CustomerBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_CustomerBrwUC");
                        frm_CustomerBrwUC.ParentForm = this;
                        frm_CustomerBrwUC.storecd = Settings.StoreCode;
                        frm_CustomerBrwUC.PopulateCustomerStatus();
                        frm_CustomerBrwUC.FetchData(frm_CustomerBrwUC.cmbFilter.Text);
                        lbHeading.Text = Properties.Resources.List_Customers;
                    }

                }
                else if (((SecurityPermission.AccessGroupScreen) && (intSetNavBarCustomer == 2)) || (SystemVariables.CurrentUserID <= 0))
                {
                   
                    if (frm_GroupBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_GroupBrwUC");
                        frm_GroupBrwUC.ParentForm = this;
                        frm_GroupBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_Groups;
                    }

                }
                else if (((SecurityPermission.AccessClassScreen) && (intSetNavBarCustomer == 3)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_ClassBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_ClassBrwUC");
                        frm_ClassBrwUC.ParentForm = this;
                        frm_ClassBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_Classes;
                    }

                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }

            if (wp == mnuItem)
            {
                HideAllBrowseForm("frm_SMItemBrwUC");
                frm_SMItemBrwUC.ParentForm = this;
                frm_SMItemBrwUC.LoadSubmenu();

                /*
                if (((SecurityPermission.AccessProductScreen) && (intSetNavBarProduct == 1)) || (SystemVariables.CurrentUserID <= 0))
                {
                   
                    if (frmProductBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frmProductBrwUC");
                        frmProductBrwUC.ParentForm = this;
                        frmProductBrwUC.PopulateProductStatus();
                        frmProductBrwUC.IsPOS = false;
                        frmProductBrwUC.FetchData(false, frmProductBrwUC.cmbFilter.Text);
                        lbHeading.Text = Properties.Resources.List_Products;
                    }

                }
                else if (((SecurityPermission.AccessBrandScreen) && (intSetNavBarProduct == 2)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_BrandBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_BrandBrwUC");
                        frm_BrandBrwUC.ParentForm = this;
                        frm_BrandBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_Families;
                    }

                }
                else if (((SecurityPermission.AccessDepartmentScreen) && (intSetNavBarProduct == 3)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_DepartmentBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_DepartmentBrwUC");
                        frm_DepartmentBrwUC.ParentForm = this;
                        frm_DepartmentBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_Departments;
                    }

                }
                else if (((SecurityPermission.AccessCategoryScreen) && (intSetNavBarProduct == 4)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_CategoryBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_CategoryBrwUC");
                        frm_CategoryBrwUC.ParentForm = this;
                        frm_CategoryBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_Categories;
                        frm_CategoryBrwUC.EnableDisableButton();
                    }

                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }

            if (wp == mnuOrdering)
            {
                HideAllBrowseForm("frm_SMOrderingBrwUC");
                frm_SMOrderingBrwUC.ParentForm = this;
                frm_SMOrderingBrwUC.LoadSubmenu();

                /*
                if (((SecurityPermission.AccessVendorScreen) && (intSetNavBarOrder == 1)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_VendorBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_VendorBrwUC");
                        frm_VendorBrwUC.ParentForm = this;
                        frm_VendorBrwUC.FetchData();
                        lbHeading.Text = "List of Vendors";
                    }
                }
                else if (((SecurityPermission.AccessReorderReportScreen) && (intSetNavBarOrder == 2)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_DummyBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_DummyBrwUC");
                        frm_DummyBrwUC.ParentForm = this;
                        frm_DummyBrwUC.MenuHeader = "Ordering";
                        lbHeading.Text = "Reorder Reports";
                        (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                        Report.frm_ReorderReportDlg frm_ = new Report.frm_ReorderReportDlg();
                        try
                        {
                            frm_.ShowDialog();
                        }
                        finally
                        {
                            frm_.Close();
                            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                else if (((SecurityPermission.AccessPrintLabelScreen) && (intSetNavBarOrder == 3)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_PrintLabelBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_PrintLabelBrwUC");
                        frm_PrintLabelBrwUC.ParentForm = this;
                        frm_PrintLabelBrwUC.FetchData();
                        lbHeading.Text = "Print Labels";
                    }
                }
                else if (((SecurityPermission.AccessPurchaseOrderScreen) && (intSetNavBarOrder == 4)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_POBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_POBrwUC");
                        frm_POBrwUC.ParentForm = this;
                        frm_POBrwUC.Flag = false;
                        frm_POBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                        frm_POBrwUC.TDate.EditValue = DateTime.Today.Date;
                        frm_POBrwUC.PopulateVendor();
                        frm_POBrwUC.Flag = true;
                        frm_POBrwUC.SetDecimalPlace();
                        frm_POBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_POBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_POBrwUC.TDate.EditValue.ToString()));
                        lbHeading.Text = Properties.Resources.List_of_Purchase_Orders;
                    }
                }
                else if (((SecurityPermission.AccessReceivingScreen) && (intSetNavBarOrder == 5)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_ReceivingBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_ReceivingBrwUC");
                        frm_ReceivingBrwUC.ParentForm = this;
                        frm_ReceivingBrwUC.Flag = false;
                        frm_ReceivingBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                        frm_ReceivingBrwUC.TDate.EditValue = DateTime.Today.Date;
                        frm_ReceivingBrwUC.PopulateVendor();
                        frm_ReceivingBrwUC.Flag = true;
                        frm_ReceivingBrwUC.SetDecimalPlace();
                        frm_ReceivingBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_ReceivingBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_ReceivingBrwUC.TDate.EditValue.ToString()));
                        lbHeading.Text = Properties.Resources.Receiving_Items;
                    }
                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }

            if (wp == mnuEmployee)
            {
                HideAllBrowseForm("frm_SMEmployeeBrwUC");
                frm_SMEmployeeBrwUC.ParentForm = this;
                frm_SMEmployeeBrwUC.LoadSubmenu();

                /*
                if (((SecurityPermission.AccessEmployeeScreen) && (intSetNavBarEmployee == 1)) || (SystemVariables.CurrentUserID <= 0) || (SystemVariables.CurrentUserID == -1))
                {
                    
                    if (frm_EmployeeBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_EmployeeBrwUC");
                        frm_EmployeeBrwUC.ParentForm = this;
                        frm_EmployeeBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_of_Employees;
                    }
                }
                else if (((SecurityPermission.AccessShiftScreen) && (intSetNavBarEmployee == 2)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frmShiftBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frmShiftBrwUC");
                        frmShiftBrwUC.ParentForm = this;
                        frmShiftBrwUC.FetchGridData();
                        lbHeading.Text = Properties.Resources.List_of_Shifts;
                    }
                }
                else if (((SecurityPermission.AccessHolidayScreen) && (intSetNavBarEmployee == 3)) || (SystemVariables.CurrentUserID <= 0))
                {

                    if (frmHolidayBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frmHolidayBrwUC");
                        frmHolidayBrwUC.ParentForm = this;
                        frmHolidayBrwUC.Year = DateTime.Today.Year;
                        frmHolidayBrwUC.PopulateYear();
                        lbHeading.Text = Properties.Resources.List_of_Holidays;
                    }
                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }



            if (wp == mnuSettings)
            {
                HideAllBrowseForm("frm_SMAdminBrwUC");
                frm_SMAdminBrwUC.ParentForm = this;
                frm_SMAdminBrwUC.LoadSubmenu();

                /*
                if (((SecurityPermission.AccessTaxScreen) && (intSetNavBarSetup == 1)) || ((SystemVariables.CurrentUserID <= 0) && (Settings.RegPOSAccess == "Y")))
                {
                    
                    if (frm_TaxBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_TaxBrwUC");
                        frm_TaxBrwUC.ParentForm = this;
                        frm_TaxBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_of_Taxes;
                    }
                }
                else if (((SecurityPermission.AccessTenderTypeScreen) && (intSetNavBarSetup == 2)) || ((SystemVariables.CurrentUserID <= 0) && (Settings.RegPOSAccess == "Y")))
                {
                    
                    if (frm_TenderTypesBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_TenderTypesBrwUC");
                        frm_TenderTypesBrwUC.ParentForm = this;
                        frm_TenderTypesBrwUC.FetchData();
                        lbHeading.Text = "List of Tender Types";
                        frm_TenderTypesBrwUC.EnableDisableButton();
                    }
                }
                else if (((SecurityPermission.AccessSecurityScreen) && (intSetNavBarSetup == 3)) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_SecurityGroupBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_SecurityGroupBrwUC");
                        frm_SecurityGroupBrwUC.ParentForm = this;
                        frm_SecurityGroupBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.Security_Profiles;
                    }
                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }

            if (wp == mnuDiscounts)
            {
                HideAllBrowseForm("frm_SMDiscountBrwUC");
                frm_SMDiscountBrwUC.ParentForm = this;
                frm_SMDiscountBrwUC.LoadSubmenu();
                /*
                if ((SecurityPermission.AccessDiscountTab) || (SystemVariables.CurrentUserID <= 0))
                {
                   
                    if (frm_DiscountBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_DiscountBrwUC");
                        frm_DiscountBrwUC.ParentForm = this;
                        frm_DiscountBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.List_of_Discounts;
                    }
                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }

            if (wp == mnuHost)
            {
                HideAllBrowseForm("frm_SMHostBrwUC");
                frm_SMHostBrwUC.ParentForm = this;
                frm_SMHostBrwUC.LoadSubmenu();
                /*
                if ((SecurityPermission.AccessCentralOffice) || (SystemVariables.CurrentUserID <= 0))
                {
                    
                    if (frm_InventoryBrwUC.Visibility == Visibility.Collapsed)
                    {
                        HideAllBrowseForm("frm_InventoryBrwUC");
                        frm_InventoryBrwUC.ParentForm = this;
                        frm_InventoryBrwUC.storecd = Settings.StoreCode;
                        frm_InventoryBrwUC.PopulateStore();
                        frm_InventoryBrwUC.PopulateSKU();
                        frm_InventoryBrwUC.FetchData();
                        lbHeading.Text = Properties.Resources.Inventory_Status;
                    }
                }
                else
                {
                    //DocMessage.MsgPermission1();
                }*/
            }
        }

        private void SetSeletedItem(WrapPanel wp, string itemname)
        {
            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == itemname)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }

                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
            }
        }

        private void ResetTagOfGroupItem(WrapPanel wp)
        {
            foreach (UIElement uI in wp.Children)
            {
                (uI as DockPanel).Tag = "0";

                if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                foreach (UIElement ulsub in (uI as DockPanel).Children)
                {
                    if (ulsub is Grid)
                    {
                        foreach (UIElement cr in (ulsub as Grid).Children)
                        {
                            if (cr is TextBlock)
                            {
                                (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                            }

                            if (cr is Image)
                            {
                                (cr as Image).Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }



        private void SetItemsPanelFromSelectedGroupSpl(DockPanel dp)
        {
            HideAllGroupItems1();

            foreach (UIElement uI in MenuPanel.Children)
            {
                //if ((uI as DockPanel).Tag.ToString() == "99") continue;
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
                    /*
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is TextBlock)
                        {
                            (ulsub as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                            lbselectedGrp.Text = (ulsub as TextBlock).Text;
                        }
                    }*/
                }
                else
                {
                    (uI as DockPanel).Tag = "0";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));

                    /*foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is TextBlock)
                        {
                            (ulsub as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                        }
                    }*/
                }
            }




        }


        private void Itm_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            bool blProceed = true;
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {

                if (((uI as DockPanel).Tag.ToString() == "1") && ((uI as DockPanel).Name == dp.Name))
                {
                    blProceed = false;
                    break;
                }
            }

            if (blProceed)
            {
                foreach (UIElement uI in wp.Children)
                {
                    if ((uI as DockPanel).Name == dp.Name)
                    {
                        (uI as DockPanel).Tag = "1";
                        if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                        if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                        foreach (UIElement ulsub in (uI as DockPanel).Children)
                        {
                            if (ulsub is Grid)
                            {
                                foreach (UIElement cr in (ulsub as Grid).Children)
                                {
                                    if (cr is TextBlock)
                                    {
                                        if (SystemVariables.SelectedTheme == "Dark")
                                        {
                                            (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                            (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                        }
                                        if (SystemVariables.SelectedTheme == "Light")
                                        {
                                            (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                        }
                                    }

                                    if (cr is Image)
                                    {
                                        (cr as Image).Visibility = Visibility.Visible;
                                    }
                                }
                            }

                        }

                    }
                    else
                    {
                        (uI as DockPanel).Tag = "0";

                        if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                        if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));


                        foreach (UIElement ulsub in (uI as DockPanel).Children)
                        {
                            if (ulsub is Grid)
                            {
                                foreach (UIElement cr in (ulsub as Grid).Children)
                                {
                                    if (cr is TextBlock)
                                    {
                                        if (SystemVariables.SelectedTheme == "Dark")
                                        {
                                            (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                            (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                        }
                                        if (SystemVariables.SelectedTheme == "Light")
                                        {
                                            (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                        }
                                    }

                                    if (cr is Image)
                                    {
                                        (cr as Image).Visibility = Visibility.Collapsed;
                                    }
                                }
                            }

                        }
                    }
                }

                CustomItemClick(dp.Name);
            }


        }

        private void CustomItemClick(string clickitem)
        {
            /*
            if (clickitem == "nbCustomer2")
            {
                HideAllBrowseForm("frm_CustomerBrwUC");
                frm_CustomerBrwUC.ParentForm = this;
                frm_CustomerBrwUC.storecd = Settings.StoreCode;
                frm_CustomerBrwUC.PopulateCustomerStatus();
                frm_CustomerBrwUC.FetchData(frm_CustomerBrwUC.cmbFilter.Text);
                lbHeading.Text = "List of Customers" ;
            }

            if (clickitem == "nbCls2")
            {
                HideAllBrowseForm("frm_ClassBrwUC");
                frm_ClassBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Classes;
            }

            if (clickitem == "nbGrp2")
            {
                HideAllBrowseForm("frm_GroupBrwUC");
                frm_GroupBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Groups;
            }

            if (clickitem == "nbProduct2")
            {
                HideAllBrowseForm("frmProductBrwUC");
                frmProductBrwUC.PopulateProductStatus();
                frmProductBrwUC.IsPOS = false;
                frmProductBrwUC.FetchData(false, frmProductBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_Products;
            }

            if (clickitem == "nbService2")
            {

                HideAllBrowseForm("frm_ServiceBrwUC");
                frm_ServiceBrwUC.PopulateServiceStatus();
                frm_ServiceBrwUC.IsPOS = false;
                frm_ServiceBrwUC.FetchData(false, frm_ServiceBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_Services;
            }

            if (clickitem == "nbBrand2")
            {
                HideAllBrowseForm("frm_BrandBrwUC");
                frm_BrandBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Families;
            }

            if (clickitem == "nbDept2")
            {
                HideAllBrowseForm("frm_DepartmentBrwUC");
                frm_DepartmentBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Departments;
            }

            if (clickitem == "nbCat2")
            {
                HideAllBrowseForm("frm_CategoryBrwUC");
                frm_CategoryBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Categories;
                frm_CategoryBrwUC.EnableDisableButton();
            }

            if (clickitem == "nbJournal2")
            {
                HideAllBrowseForm("frm_JournalBrwUC");
                frm_JournalBrwUC.Flag = false;
                frm_JournalBrwUC.FDate.EditValue = DateTime.Today.Date;
                frm_JournalBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_JournalBrwUC.PopulateProduct();
                frm_JournalBrwUC.PopulateAction();
                frm_JournalBrwUC.Flag = true;
                lbHeading.Text = Properties.Resources.List_Stock_Journal;

                frm_JournalBrwUC.FetchData(GeneralFunctions.fnDate(frm_JournalBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_JournalBrwUC.TDate.EditValue.ToString()), frm_JournalBrwUC.cmbAction.EditValue.ToString(),
                                         GeneralFunctions.fnInt32(frm_JournalBrwUC.cmbItem.EditValue));
            }

            if (clickitem == "nbStockTake2")
            {
                HideAllBrowseForm("frm_StocktakeBrwUC");
                lbHeading.Text = Properties.Resources.List_Inventory_Adjustment;
                frm_StocktakeBrwUC.Flag = false;
                frm_StocktakeBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_StocktakeBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_StocktakeBrwUC.Flag = true;
                frm_StocktakeBrwUC.FetchData(GeneralFunctions.fnDate(frm_StocktakeBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_StocktakeBrwUC.TDate.EditValue.ToString()));
            }

            if (clickitem == "nbPurchaseOrder2")
            {
                HideAllBrowseForm("frm_POBrwUC");
                frm_POBrwUC.Flag = false;
                frm_POBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_POBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_POBrwUC.PopulateVendor();
                frm_POBrwUC.Flag = true;
                frm_POBrwUC.SetDecimalPlace();
                frm_POBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_POBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_POBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.List_of_Purchase_Orders;
            }

            if (clickitem == "nbReceiving2")
            {
                HideAllBrowseForm("frm_ReceivingBrwUC");
                frm_ReceivingBrwUC.Flag = false;
                frm_ReceivingBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_ReceivingBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_ReceivingBrwUC.PopulateVendor();
                frm_ReceivingBrwUC.Flag = true;
                frm_ReceivingBrwUC.SetDecimalPlace();
                frm_ReceivingBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_ReceivingBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_ReceivingBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.Receiving_Items;
            }

            if (clickitem == "nbTransfer2")
            {
                HideAllBrowseForm("frm_TransferBrwUC");
                frm_TransferBrwUC.Flag = false;
                frm_TransferBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_TransferBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_TransferBrwUC.Flag = true;
                frm_TransferBrwUC.SetDecimalPlace();
                frm_TransferBrwUC.FetchData(GeneralFunctions.fnDate(frm_TransferBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_TransferBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.Transfer_Items;
            }

            if (clickitem == "nbVendors2")
            {
                HideAllBrowseForm("frm_VendorBrwUC");
                frm_VendorBrwUC.FetchData();
                lbHeading.Text = "List of Vendors";
            }

            if (clickitem == "nbPrintLabel2")
            {
                HideAllBrowseForm("frm_PrintLabelBrwUC");
                frm_PrintLabelBrwUC.FetchData();
                lbHeading.Text = "Print Labels";
            }

            if (clickitem == "nbZips2")
            {
                HideAllBrowseForm("frmZipCodeBrwUC");
                frmZipCodeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Zipcodes;
            }

            if (clickitem == "nbSale2")
            {
                HideAllBrowseForm("frm_SalePriceBrwUC");
                frm_SalePriceBrwUC.PopulateSaleBatchFilters();
                frm_SalePriceBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Sale_Price;
            }

            if (clickitem == "nbFuturePrice2")
            {
                HideAllBrowseForm("frm_FuturePriceBrwUC");
                frm_FuturePriceBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Future_Price;
            }

            if (clickitem == "nbDiscount2")
            {
                HideAllBrowseForm("frm_DiscountBrwUC");
                frm_DiscountBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Discounts;
            }

            if (clickitem == "nbBuynGetFree2")
            {
                HideAllBrowseForm("frm_BuynGetFreeBrwUC");
                frm_BuynGetFreeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Buy_n_Get_Free_items;
            }

            if (clickitem == "nbMixNmatch2")
            {
                HideAllBrowseForm("frm_Mix_n_MatchBrwUC");
                frm_Mix_n_MatchBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Mix_n_Match_items;
            }

            if (clickitem == "nbBreakPack2")
            {
                HideAllBrowseForm("frm_BreakPackBrwUC");
                frm_BreakPackBrwUC.PopulateBreakPackStatus();
                frm_BreakPackBrwUC.IsPOS = false;
                frm_BreakPackBrwUC.FetchData(false, frm_BreakPackBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_of_Break_Packs;
            }

            if (clickitem == "nbEmp2")
            {
                HideAllBrowseForm("frm_EmployeeBrwUC");
                frm_EmployeeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Employees;
            }

            if (clickitem == "nbShift2")
            {
                HideAllBrowseForm("frmShiftBrwUC");
                frmShiftBrwUC.FetchGridData();
                lbHeading.Text = Properties.Resources.List_of_Shifts;
            }

            if (clickitem == "nbHoliday2")
            {
                HideAllBrowseForm("frmHolidayBrwUC");
                frmHolidayBrwUC.Year = DateTime.Today.Year;
                frmHolidayBrwUC.PopulateYear();
                lbHeading.Text = Properties.Resources.List_of_Holidays;
            }

            if (clickitem == "nbGL2")
            {
                HideAllBrowseForm("frm_GLBrwUC");
                frm_GLBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_GL_Accounts;
            }

            if (clickitem == "nbTax2")
            {
                HideAllBrowseForm("frm_TaxBrwUC");
                frm_TaxBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Taxes;
            }

            if (clickitem == "nbTenderTypes2")
            {
                HideAllBrowseForm("frm_TenderTypesBrwUC");
                frm_TenderTypesBrwUC.FetchData();
                lbHeading.Text = "List of Tender Types";
                frm_TenderTypesBrwUC.EnableDisableButton();
            }

            if (clickitem == "nbSecurity2")
            {
                HideAllBrowseForm("frm_SecurityGroupBrwUC");
                frm_SecurityGroupBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Security_Profiles;
            }

            if (clickitem == "nbFees2")
            {
                HideAllBrowseForm("frm_FeesBrwUC");
                frm_FeesBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Fees___Charges;
            }

            if (clickitem == "nbPriceAdjustment2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = Properties.Resources.Price_Adjustment;
            }

            if (clickitem == "nbReorder2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = "Reorder Reports";
            }

            if (clickitem == "nbDefinedParam2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = Properties.Resources.Dynamic_CRM_Parameters;
            }

            if (clickitem == "nbRegistration2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = Properties.Resources.Registration;
            }

            if (clickitem == "nbGeneralSetup2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = Properties.Resources.General_Settings;
            }

            if (clickitem == "nbGwareHostSetup2")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                lbHeading.Text = Properties.Resources.XEPOS_Host_Setup;
            }

            if (clickitem == "nbCOcustomer2")
            {
                HideAllBrowseForm("frm_ImportCustomerBrwUC");
                frm_CustomerBrwUC.storecd = Settings.StoreCode;
                frm_ImportCustomerBrwUC.PopulateStore();
                frm_ImportCustomerBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Import_Customers;
            }

            if (clickitem == "nbCOInventory2")
            {
                HideAllBrowseForm("frm_InventoryBrwUC");
                frm_InventoryBrwUC.storecd = Settings.StoreCode;
                frm_InventoryBrwUC.PopulateStore();
                frm_InventoryBrwUC.PopulateSKU();
                frm_InventoryBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Inventory_Status;
            }

            if (clickitem == "nbGC2")
            {
                HideAllBrowseForm("frm_GiftCertBrwUC");
                frm_GiftCertBrwUC.FetchGC();
                lbHeading.Text = Properties.Resources.Gift_Certificates;
            }
            */
        }

        private void NavGroupExit1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Do you want to Exit?") == MessageBoxResult.Yes)
            {
                SystemVariables.CurrentUserID = -1;
                SystemVariables.CurrentUserName = "";
                (Window.GetWindow(this) as MainWindow).LoggedInUserTextBlock.Text = Properties.Resources.NoLoggedInUser;
                this.Visibility = Visibility.Collapsed;
                (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
                (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
                (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
                (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
                //(Window.GetWindow(this) as MainWindow).LoginSection = LoginSection.None;
                //(Window.GetWindow(this) as MainWindow).LoginControl.txtUser.Text = "";
                //(Window.GetWindow(this) as MainWindow).LoginControl.txtPswd.PasswordBox.Password = "";
                (Window.GetWindow(this) as MainWindow).UpdateLayout();
            }
        }

        private void NbDefinedParam1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = Properties.Resources.Dynamic_CRM_Parameters;

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_ClientParamDlg frm_ = new POSSection.frm_ClientParamDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbPriceAdjustment1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = Properties.Resources.Price_Adjustment;

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PriceAdjustmentDlg frm_ = new frm_PriceAdjustmentDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbGeneralSetup1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = Properties.Resources.General_Settings;

            if (IsPOSInstalled())
            {
                CheckAccessForGeneralSettings("31z1");
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_GeneralSetupDlg frm_GeneralSetupDlg = new Administrator.frm_GeneralSetupDlg();
                try
                {
                    frm_GeneralSetupDlg.CallFromAdmin = true;
                    frm_GeneralSetupDlg.bLoad = false;
                    frm_GeneralSetupDlg.MainF = this;
                    frm_GeneralSetupDlg.FunctionOrderChangeAccess = blFunctionOrderChangeAccess;
                    frm_GeneralSetupDlg.FunctionBtnAccess = blFunctionBtnAccess;
                    frm_GeneralSetupDlg.ShowDialog();
                }
                finally
                {

                    string ModulesRegistered = GeneralFunctions.RegisteredModules();

                    if (!ModulesRegistered.Contains("SCALE"))
                    {

                    }
                }
                if (frm_GeneralSetupDlg.ThemeCHanged)
                {
                    SetItemsPanelFromSelectedGroupSpl(mnuSettings);
                }
                frm_GeneralSetupDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                DocMessage.MsgInformation("You must install POS for .Net");
            }
        }

        private void NbGwareHostSetup1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = Properties.Resources.XEPOS_Host_Setup;

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Administrator.frm_GwareHostSetupDlg frm_ = new Administrator.frm_GwareHostSetupDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbRegistration1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = Properties.Resources.Registration;

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmRegistrationDlg frm_RegistrationDlg = new frmRegistrationDlg();
            try
            {
                frm_RegistrationDlg.FirstTimeCall = false;
                frm_RegistrationDlg.ShowDialog();
                if (frm_RegistrationDlg.DialogResult == true)
                {
                    if (frm_RegistrationDlg.Registered)
                    {
                        //StatusItem4.Caption = " " + Translation.SetMultilingualTextInCodes("Registered to :", "frmMain_Registeredto") + " " + Settings.RegCompanyName;
                        DocMessage.MsgInformation("Successful Registration, Terminating Application...");
                        Application.Current.Shutdown();
                    }
                }
            }
            finally
            {
                frm_RegistrationDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void NbReorder1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            WrapPanel wp = dp.Parent as WrapPanel;

            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3ECEF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                    }
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5FA6B4"));
                                    }
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
                else
                {
                    (uI as DockPanel).Tag = "0";

                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F3A4D"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSRegular") as FontFamily;
                                    if (SystemVariables.SelectedTheme == "Dark") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                                    if (SystemVariables.SelectedTheme == "Light") (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#686868"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                    }
                }
            }

            HideAllBrowseForm("frm_DummyBrwUC");
            lbHeading.Text = "Reorder Reports";

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            Report.frm_ReorderReportDlg frm_ = new Report.frm_ReorderReportDlg();
            try
            {
                frm_.ShowDialog();
            }
            finally
            {
                frm_.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnECHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pnlMenuHeader.Width > 50.0)
            {
                pnlMenuHeader.Width = 50.0;
                imghide.Visibility = Visibility.Collapsed;
                imgshow.Visibility = Visibility.Visible;
                lbShowHideH.Text = "Show";
                btnECHeader.SetValue(Canvas.LeftProperty, -10.0);
            }
            else
            {
                pnlMenuHeader.Width = 140.0;
                imghide.Visibility = Visibility.Visible;
                imgshow.Visibility = Visibility.Collapsed;
                lbShowHideH.Text = "Hide";
                btnECHeader.SetValue(Canvas.LeftProperty, 80.0);
            }
        }

        private void SetCustomNavBar1()
        {
            int intNavID = 0;
            int intNavCustomer = 0;
            int intNavProduct = 0;
            int intNavOrder = 0;
            int intNavEmp = 0;
            int intNavSetup = 0;
            int intNavOther = 0;
            int intNavCO = 0;
            int intNavDiscount = 0;
            int intNavLabel = 0;

            mnuCustomer.Visibility = Visibility.Visible;
            mnuItem.Visibility = Visibility.Visible;
            mnuOrdering.Visibility = Visibility.Visible;
            mnuReport.Visibility = Visibility.Visible;
            mnuEmployee.Visibility = Visibility.Visible;
            mnuSettings.Visibility = Visibility.Visible;
            mnuDiscounts.Visibility = Visibility.Visible;
            mnuHost.Visibility = Visibility.Visible;

            mnuCustomer.Tag = "0";
            mnuItem.Tag = "0";
            mnuOrdering.Tag = "0";
            mnuReport.Tag = "0";
            mnuEmployee.Tag = "0";
            mnuSettings.Tag = "0";
            mnuDiscounts.Tag = "0";
            mnuHost.Tag = "0";





            lbHeading.Text = "";



            DevExpress.Xpf.NavBar.NavBarGroup selectActiveNavbar = new DevExpress.Xpf.NavBar.NavBarGroup();



            if (Settings.CloseoutExport == "Y")
            {
                Settings.EnableSpecificSubMenu("Administrator", "G/L Account");
                //nbGL2.Visibility = Visibility.Visible;
            }
            else
            {
                Settings.DisableSpecificSubMenu("Administrator", "G/L Account");
                //nbGL2.Visibility = Visibility.Collapsed;
                //tabSetup1.Height = tabSetup1.Height - 41;
            }


            if (SystemVariables.CurrentUserID == -1)
            {
                SelectedDocPanel = mnuEmployee;
                return;
            }

            if (SystemVariables.CurrentUserID <= 0)
            {
                if (Settings.CentralExportImport == "Y")
                {
                    if (!SecurityPermission.AccessCentralOffice)
                    {
                        //mnuHost.Visibility = Visibility.Visible;
                        Settings.EnableSpecificMenuGroup("Host");
                    }
                    //nbTransfer2.Visibility = Visibility.Visible;
                    Settings.EnableSpecificSubMenu("Ordering", "Transfer");
                }
                else
                {
                    Settings.DisableSpecificMenuGroup("Host");
                    Settings.DisableSpecificSubMenu("Ordering", "Transfer");
                    //mnuHost.Visibility = Visibility.Collapsed;
                    //nbTransfer2.Visibility = Visibility.Collapsed;
                    //tabOrder1.Height = tabOrder1.Height - 41;
                }

                if (GeneralFunctions.GetRecordCount("StoreInfo") == 0)
                {
                    SelectedDocPanel = mnuSettings;
                    return;
                }
                else
                {
                    SelectedDocPanel = mnuItem;
                    return;
                }
            }
            else
            {
                string ModulesRegistered = GeneralFunctions.RegisteredModules();

                if (ModulesRegistered.Contains("POS"))
                {
                    if (ModulesRegistered.Contains("POS") && !ModulesRegistered.Contains("SCALE"))
                    {
                        // Comment as not implemented nbGraphicArt.Visible = false;
                        //nbProductionList.Visible = false;
                        //nbProductionBatch.Visible = false;
                    }

                    if (Settings.CentralExportImport == "Y")
                    {
                        if (!SecurityPermission.AccessCentralOffice)
                        {
                            //mnuHost.Visibility = Visibility.Visible;
                            Settings.EnableSpecificMenuGroup("Host");
                        }
                        //nbTransfer2.Visibility = Visibility.Visible;
                        Settings.EnableSpecificSubMenu("Ordering", "Transfer");
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Host");
                        Settings.DisableSpecificSubMenu("Ordering", "Transfer");
                    }




                    if (!SecurityPermission.AccessCustomerScreen)
                    {
                        Settings.DisableSpecificMenuGroup("Customers");
                        //mnuCustomer.Visibility = Visibility.Collapsed;
                        //tabCustomer1.Height = tabCustomer1.Height - 41;
                    }
                    if (!SecurityPermission.AccessGroupScreen)
                    {
                        Settings.DisableSpecificSubMenu("Customers", "Groups");
                        //nbGrp2.Visibility = Visibility.Collapsed;
                        //tabCustomer1.Height = tabCustomer1.Height - 41;
                    }
                    if (!SecurityPermission.AccessClassScreen)
                    {
                        Settings.DisableSpecificSubMenu("Customers", "Classes");
                        //nbCls2.Visibility = Visibility.Collapsed;
                        //tabCustomer1.Height = tabCustomer1.Height - 41;
                    }
                    if (!SecurityPermission.AccessCRMParameterScreen)
                    {
                        Settings.DisableSpecificSubMenu("Customers", "CRM Parameters");
                        //nbDefinedParam2.Visibility = Visibility.Collapsed;
                        //tabCustomer1.Height = tabCustomer1.Height - 41;
                    }




                    if (!SecurityPermission.AccessProductScreen)
                    {
                        Settings.DisableSpecificMenuGroup("Items");
                        mnuItem.Visibility = Visibility.Collapsed;

                    }
                    if (!SecurityPermission.AccessBrandScreen)
                    {
                        Settings.DisableSpecificSubMenu("Items", "Families");
                        //nbBrand2.Visibility = Visibility.Collapsed;
                        //tabItem1.Height = tabItem1.Height - 41;
                    }
                    if (!SecurityPermission.AccessDepartmentScreen)
                    {
                        Settings.DisableSpecificSubMenu("Items", "Departments");
                        //nbDept2.Visibility = Visibility.Collapsed;
                        //tabItem1.Height = tabItem1.Height - 41;
                    }
                    if (!SecurityPermission.AccessCategoryScreen)
                    {
                        Settings.DisableSpecificSubMenu("Items", "POS Screen Categories");
                        //nbCat2.Visibility = Visibility.Collapsed;
                        //tabItem1.Height = tabItem1.Height - 41;
                    }
                    if (!SecurityPermission.AccessStockTakeScreen)
                    {
                        Settings.DisableSpecificSubMenu("Items", "Inventory Adjustment");
                        //nbStockTake2.Visibility = Visibility.Collapsed;
                        //tabItem1.Height = tabItem1.Height - 41;
                    }


                    if (!SecurityPermission.AccessVendorScreen)
                    {
                        Settings.DisableSpecificSubMenu("Ordering", "Vendors");
                        //nbVendors2.Visibility = Visibility.Collapsed;
                        //tabOrder1.Height = tabOrder1.Height - 41;
                    }
                    if (!SecurityPermission.AccessReorderReportScreen)
                    {
                        Settings.DisableSpecificSubMenu("Ordering", "Reorder Report");
                        //nbReorder2.Visibility = Visibility.Collapsed;
                        //tabOrder1.Height = tabOrder1.Height - 41;
                    }
                    if (!SecurityPermission.AccessPurchaseOrderScreen)
                    {
                        Settings.DisableSpecificSubMenu("Ordering", "Purchase Order");
                        //nbPurchaseOrder2.Visibility = Visibility.Collapsed;
                        //tabOrder1.Height = tabOrder1.Height - 41;
                    }
                    if (!SecurityPermission.AccessReceivingScreen)
                    {
                        Settings.DisableSpecificSubMenu("Ordering", "Receiving");
                        //nbReceiving2.Visibility = Visibility.Collapsed;
                        //tabOrder1.Height = tabOrder1.Height - 41;
                    }
                    if (!SecurityPermission.AccessPrintLabelScreen)
                    {
                        Settings.DisableSpecificSubMenu("Ordering", "Print Labels");
                        //nbPrintLabel2.Visibility = Visibility.Collapsed;
                        //tabOrder1.Height = tabOrder1.Height - 41;
                    }


                    if (!SecurityPermission.AccessEmployeeScreen)
                    {
                        Settings.DisableSpecificSubMenu("Employee", "Employees");
                        //nbEmp2.Visibility = Visibility.Collapsed;
                        //tabEmployee1.Height = tabEmployee1.Height - 41;
                    }
                    if (!SecurityPermission.AccessShiftScreen)
                    {
                        Settings.DisableSpecificSubMenu("Employee", "Shifts");
                        //nbShift2.Visibility = Visibility.Collapsed;
                        //tabEmployee1.Height = tabEmployee1.Height - 41;
                    }
                    if (!SecurityPermission.AccessHolidayScreen)
                    {
                        Settings.DisableSpecificSubMenu("Employee", "Holidays");
                        //nbHoliday2.Visibility = Visibility.Collapsed;
                        //tabEmployee1.Height = tabEmployee1.Height - 41;
                    }

                    if (!SecurityPermission.AccessTaxScreen)
                    {
                        Settings.DisableSpecificSubMenu("Administrator", "Tax");
                        //nbTax2.Visibility = Visibility.Collapsed;
                        //tabSetup1.Height = tabSetup1.Height - 41;
                    }
                    if (!SecurityPermission.AccessTenderTypeScreen)
                    {
                        Settings.DisableSpecificSubMenu("Administrator", "Tender Types");
                        //nbTenderTypes2.Visibility = Visibility.Collapsed;
                        //tabSetup1.Height = tabSetup1.Height - 41;
                    }
                    if (!SecurityPermission.AccessSecurityScreen)
                    {
                        Settings.DisableSpecificSubMenu("Administrator", "Security");
                        //nbSecurity2.Visibility = Visibility.Collapsed;
                        //tabSetup1.Height = tabSetup1.Height - 41;
                    }




                    //product
                    if (SecurityPermission.AccessProductScreen)
                    {
                        if (intNavProduct == 0) intNavProduct = 1;
                        if (intSetNavBarProduct == 0) intSetNavBarProduct = 1;
                    }

                    if (SecurityPermission.AccessBrandScreen)
                    {
                        if (intNavProduct == 0) intNavProduct = 1;
                        if (intSetNavBarProduct == 0) intSetNavBarProduct = 2;
                    }
                    if (SecurityPermission.AccessDepartmentScreen)
                    {
                        if (intNavProduct == 0) intNavProduct = 1;
                        if (intSetNavBarProduct == 0) intSetNavBarProduct = 3;
                    }
                    if (SecurityPermission.AccessCategoryScreen)
                    {
                        if (intNavProduct == 0) intNavProduct = 1;
                        if (intSetNavBarProduct == 0) intSetNavBarProduct = 4;
                    }
                    if (SecurityPermission.AccessStockTakeScreen)
                    {
                        if (intNavProduct == 0) intNavProduct = 1;
                        if (intSetNavBarProduct == 0) intSetNavBarProduct = 5;
                    }
                    if (intNavProduct == 1)
                    {
                        if (intNavID == 0) intNavID = 1;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Items");
                        mnuItem.Visibility = Visibility.Collapsed;
                    }

                    //customer
                    if (SecurityPermission.AccessCustomerScreen)
                    {
                        if (intNavCustomer == 0) intNavCustomer = 1;
                        if (intSetNavBarCustomer == 0) intSetNavBarCustomer = 1;
                    }
                    if (SecurityPermission.AccessGroupScreen)
                    {
                        if (intNavCustomer == 0) intNavCustomer = 1;
                        if (intSetNavBarCustomer == 0) intSetNavBarCustomer = 2;
                    }
                    if (SecurityPermission.AccessClassScreen)
                    {
                        if (intNavCustomer == 0) intNavCustomer = 1;
                        if (intSetNavBarCustomer == 0) intSetNavBarCustomer = 3;
                    }
                    if (SecurityPermission.AccessCRMParameterScreen)
                    {
                        if (intNavCustomer == 0) intNavCustomer = 1;
                        if (intSetNavBarCustomer == 0) intSetNavBarCustomer = 4;
                    }
                    if (intNavCustomer == 1)
                    {
                        if (intNavID == 0) intNavID = 2;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Customers");
                        mnuCustomer.Visibility = Visibility.Collapsed;
                    }

                    // Ordering
                    if (SecurityPermission.AccessVendorScreen)
                    {
                        if (intNavOrder == 0) intNavOrder = 1;
                        if (intSetNavBarOrder == 0) intSetNavBarOrder = 1;
                    }
                    if (SecurityPermission.AccessReorderReportScreen)
                    {
                        if (intNavOrder == 0) intNavOrder = 1;
                        if (intSetNavBarOrder == 0) intSetNavBarOrder = 2;
                    }
                    if (SecurityPermission.AccessPurchaseOrderScreen)
                    {
                        if (intNavOrder == 0) intNavOrder = 1;
                        if (intSetNavBarOrder == 0) intSetNavBarOrder = 3;
                    }
                    if (SecurityPermission.AccessReceivingScreen)
                    {
                        if (intNavOrder == 0) intNavOrder = 1;
                        if (intSetNavBarOrder == 0) intSetNavBarOrder = 4;
                    }
                    if (SecurityPermission.AccessPrintLabelScreen)
                    {
                        if (intNavOrder == 0) intNavOrder = 1;
                        if (intSetNavBarOrder == 0) intSetNavBarOrder = 5;
                    }

                    if (intNavOrder == 1)
                    {
                        if (intNavID == 0) intNavID = 3;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Ordering");
                        mnuOrdering.Visibility = Visibility.Collapsed;
                    }

                    // Reports
                    if (SecurityPermission.AccessReportsScreen)
                    {
                        if (intNavID == 0) intNavID = 4;
                    }
                    else
                    {
                        mnuReport.Visibility = Visibility.Collapsed;
                    }


                    //Employee
                    if (SecurityPermission.AccessEmployeeScreen)
                    {
                        if (intNavEmp == 0) intNavEmp = 1;
                        if (intSetNavBarEmployee == 0) intSetNavBarEmployee = 1;
                    }
                    if (SecurityPermission.AccessShiftScreen)
                    {
                        if (intNavEmp == 0) intNavEmp = 1;
                        if (intSetNavBarEmployee == 0) intSetNavBarEmployee = 2;
                    }
                    if (SecurityPermission.AccessHolidayScreen)
                    {
                        if (intNavEmp == 0) intNavEmp = 1;
                        if (intSetNavBarEmployee == 0) intSetNavBarEmployee = 3;
                    }

                    if (intNavEmp == 1)
                    {
                        if (intNavID == 0) intNavID = 5;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Employee");
                        mnuEmployee.Visibility = Visibility.Collapsed;
                    }

                    //Setup
                    if (SecurityPermission.AccessTaxScreen)
                    {
                        if (intNavSetup == 0) intNavSetup = 1;
                        if (intSetNavBarSetup == 0) intSetNavBarSetup = 1;
                    }
                    if (SecurityPermission.AccessTenderTypeScreen)
                    {
                        if (intNavSetup == 0) intNavSetup = 1;
                        if (intSetNavBarSetup == 0) intSetNavBarSetup = 2;
                    }
                    if (SecurityPermission.AccessSecurityScreen)
                    {
                        if (intNavSetup == 0) intNavSetup = 1;
                        if (intSetNavBarSetup == 0) intSetNavBarSetup = 3;
                    }

                    /*if (Settings.CloseoutExport == "Y")
                    {
                        if (intNavSetup == 0) intNavSetup = 1;
                        if (intSetNavBarSetup == 0) intSetNavBarSetup = 4;
                    }*/

                    if (intNavSetup == 1)
                    {
                        if (intNavID == 0) intNavID = 6;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Administrator");
                        mnuSettings.Visibility = Visibility.Collapsed;
                    }

                    //Other
                    /*
                    if (SecurityPermission.AccessScales)
                    {
                        if (intNavOther == 0) intNavOther = 1;
                        if (intSetNavBarOther == 0) intSetNavBarOther = 1;

                        nbScaleSetup.IsVisible = Settings.ScaleDevice == "Live Weight";
                    }

                    if (intNavOther == 1)
                    {
                        if (intNavID == 0) intNavID = 7;
                    }
                    else
                    {
                        navGroupOther.Visible = false;
                        navGroupComm.Visible = false;
                    }*/

                    if (Settings.CentralExportImport == "Y")
                    {
                        if (SecurityPermission.AccessCentralOffice)
                        {
                            if (intNavCO == 0) intNavCO = 1;
                        }
                        if (intNavCO == 1)
                        {
                            if (intNavID == 0) intNavID = 8;
                        }
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Host");
                        mnuHost.Visibility = Visibility.Collapsed;
                    }

                    if (SecurityPermission.AccessDiscountTab)
                    {
                        if (intNavDiscount == 0) intNavDiscount = 1;
                    }

                    if (intNavDiscount == 1)
                    {
                        if (intNavID == 0) intNavID = 9;
                    }
                    else
                    {
                        Settings.DisableSpecificMenuGroup("Discounts");
                        mnuDiscounts.Visibility = Visibility.Collapsed;
                    }


                    if (intNavLabel == 1)
                    {
                        if (intNavID == 0) intNavID = 10;
                    }




                    if (intNavID == 0)
                    {
                        SelectedDocPanel = null;
                    }
                    else
                    {
                        if (intNavID == 1)
                        {
                            SelectedDocPanel = mnuItem;
                        }
                        if (intNavID == 2)
                        {
                            SelectedDocPanel = mnuCustomer;
                        }
                        if (intNavID == 3)
                        {
                            SelectedDocPanel = mnuOrdering;
                        }
                        if (intNavID == 4)
                        {
                            SelectedDocPanel = mnuReport;
                        }
                        if (intNavID == 5)
                        {
                            SelectedDocPanel = mnuEmployee;
                        }
                        if (intNavID == 6)
                        {
                            SelectedDocPanel = mnuSettings;
                        }
                        if (intNavID == 8)
                        {
                            SelectedDocPanel = mnuHost;
                        }
                        if (intNavID == 9)
                        {
                            SelectedDocPanel = mnuDiscounts;
                        }

                        if (GeneralFunctions.GetRecordCount("StoreInfo") == 0)
                        {
                            SelectedDocPanel = mnuSettings;
                        }
                    }

                }

                return;
            }


        }

        public void RedirectToSubmenu(string MenuGroup)
        {
            if (MenuGroup == "Customers")
            {
                HideAllBrowseForm("frm_SMCustomerBrwUC");
                frm_SMCustomerBrwUC.ParentForm = this;
                frm_SMCustomerBrwUC.LoadSubmenu();
            }
            if (MenuGroup == "Items")
            {
                HideAllBrowseForm("frm_SMItemBrwUC");
                frm_SMItemBrwUC.ParentForm = this;
                frm_SMItemBrwUC.LoadSubmenu();
            }
            if (MenuGroup == "Ordering")
            {
                HideAllBrowseForm("frm_SMOrderingBrwUC");
                frm_SMOrderingBrwUC.ParentForm = this;
                frm_SMOrderingBrwUC.LoadSubmenu();
            }
            if (MenuGroup == "Employee")
            {
                HideAllBrowseForm("frm_SMEmployeeBrwUC");
                frm_SMEmployeeBrwUC.ParentForm = this;
                frm_SMEmployeeBrwUC.LoadSubmenu();
            }
            if (MenuGroup == "Administrator")
            {
                HideAllBrowseForm("frm_SMAdminBrwUC");
                frm_SMAdminBrwUC.ParentForm = this;
                frm_SMAdminBrwUC.LoadSubmenu();
            }

            if (MenuGroup == "Receipt Template")
            {
                HideAllBrowseForm("frm_SMAdminBrwUC");
                frm_SMAdminBrwUC.ParentForm = this;
                frm_SMAdminBrwUC.LoadSubmenu();
            }

            if (MenuGroup == "Discounts")
            {
                HideAllBrowseForm("frm_SMDiscountBrwUC");
                frm_SMDiscountBrwUC.ParentForm = this;
                frm_SMDiscountBrwUC.LoadSubmenu();
            }
            if (MenuGroup == "Host")
            {
                HideAllBrowseForm("frm_SMHostBrwUC");
                frm_SMHostBrwUC.ParentForm = this;
                frm_SMHostBrwUC.LoadSubmenu();
            }
        }

        public void ExecuteClickFromSubMenu(string clickitem)
        {

            if (clickitem == "Check New Version")
            {
                (Window.GetWindow(this) as MainWindow).StopVersionTimer();
                try
                {
                    bool boolconnect = true;
                    bool fileexists = false;
                    if (IsUpdateAvailable(ref boolconnect, ref fileexists) != "")
                    {
                        HideAllBrowseForm("frm_DummyBrwUC");
                        frm_DummyBrwUC.ParentForm = this;
                        //frm_DummyBrwUC.MenuHeader = "Customers";
                        //usrctrlVU = new POSControls.VersionUpdate();

                         (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                         POSSection.frm_VersionUpdateDlg frm_ = new POSSection.frm_VersionUpdateDlg();
                         try
                         {
                             frm_.ShowDialog();
                         }
                         finally
                         {
                             frm_.Close();
                            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                            if (frm_.UpdateComplete)
                            {
                                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                                (Window.GetWindow(this) as MainWindow).ShutDown();
                            }
                         }
                    }
                    else
                    {
                        if (boolconnect)
                        {
                            if (fileexists)
                                DocMessage.MsgInformation("You are using Current Version");
                        }
                    }
                }
                catch
                {
                    //frm_DummyBrwUC.pnlAutoUpdate.Visibility = Visibility.Collapsed;
                }
                finally
                {
                    //frm_DummyBrwUC.pnlAutoUpdate.Visibility = Visibility.Collapsed;
                    (Window.GetWindow(this) as MainWindow).RestartVersionTimer();
                }
                /*HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Receipt Template";
                lbHeading.Text = "Receipt Template";

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_ReceiptTemplateDlg frm_ = new Administrator.frm_ReceiptTemplateDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }*/
            }

            if (clickitem == "Designer")
            {
                HideAllBrowseForm("CustomReportDesigner");
               // CustomReportDesigner.ParentForm = this;
               // CustomReportDesigner.FetchData();
                lbHeading.Text = Properties.Resources.List_Future_Price;
            }

            if (clickitem == "Receipt Template")
            {
               

                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Receipt Template";
                lbHeading.Text = "Receipt Template";

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_ReceiptTemplateDlg frm_ = new Administrator.frm_ReceiptTemplateDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (clickitem == "Printer Types")
            {

                if (frm_PrinterTypeBrwUC == null)
                {
                    frm_PrinterTypeBrwUC = new Administrator.frm_PrinterTypeBrwUC();
                    frm_PrinterTypeBrwUC.Name = "frm_PrinterTypeBrwUC";
                    pnlBody.Children.Add(frm_PrinterTypeBrwUC);
                }

                HideAllBrowseForm("frm_PrinterTypeBrwUC");
                frm_PrinterTypeBrwUC.ParentForm = this;
                frm_PrinterTypeBrwUC.FetchData();
                lbHeading.Text = "List of Printer Types";
            }

            if (clickitem == "Printer Template Mapping")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Administrator";
                lbHeading.Text = "Printer Template Mapping";

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_PrinterTemplateActivationDlg frm_ = new Administrator.frm_PrinterTemplateActivationDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (clickitem == "Printer Templates")
            {

                if (frm_PrinterTypeBrwUC == null)
                {
                    frm_PrinterTemplateBrwUC = new Administrator.frm_PrinterTemplateBrwUC();
                    frm_PrinterTemplateBrwUC.Name = "frm_PrinterTemplateBrwUC";
                    pnlBody.Children.Add(frm_PrinterTemplateBrwUC);
                }

                HideAllBrowseForm("frm_PrinterTemplateBrwUC");
                frm_PrinterTemplateBrwUC.ParentForm = this;
                frm_PrinterTemplateBrwUC.FetchData();
                lbHeading.Text = "List of Printer Templates";
            }

            if (clickitem == "Customers")
            {

                if (frm_CustomerBrwUC == null)
                {
                    frm_CustomerBrwUC = new POSSection.frm_CustomerBrwUC();
                    frm_CustomerBrwUC.Name = "frm_CustomerBrwUC";
                    pnlBody.Children.Add(frm_CustomerBrwUC);
                }

                HideAllBrowseForm("frm_CustomerBrwUC");
                frm_CustomerBrwUC.ParentForm = this;
                frm_CustomerBrwUC.storecd = Settings.StoreCode;
                frm_CustomerBrwUC.PopulateCustomerStatus();
                frm_CustomerBrwUC.FetchData(frm_CustomerBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_Customers;
            }

            if (clickitem == "Classes")
            {
                if (frm_ClassBrwUC == null)
                {
                    frm_ClassBrwUC = new POSSection.frm_ClassBrwUC();
                    frm_ClassBrwUC.Name = "frm_ClassBrwUC";
                    pnlBody.Children.Add(frm_ClassBrwUC);
                }

                HideAllBrowseForm("frm_ClassBrwUC");
                frm_ClassBrwUC.ParentForm = this;
                frm_ClassBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Classes;
            }

            if (clickitem == "Groups")
            {
                if (frm_GroupBrwUC == null)
                {
                    frm_GroupBrwUC = new POSSection.frm_GroupBrwUC();
                    frm_GroupBrwUC.Name = "frm_GroupBrwUC";
                    pnlBody.Children.Add(frm_GroupBrwUC);
                }

                HideAllBrowseForm("frm_GroupBrwUC");
                frm_GroupBrwUC.ParentForm = this;
                frm_GroupBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Groups;
            }

            if (clickitem == "CRM Parameters")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Customers";
                lbHeading.Text = Properties.Resources.Dynamic_CRM_Parameters;

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                POSSection.frm_ClientParamDlg frm_ = new POSSection.frm_ClientParamDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }







            if (clickitem == "Products")
            {
                if (frmProductBrwUC == null)
                {
                    frmProductBrwUC = new POSSection.frmProductBrwUC();
                    frmProductBrwUC.Name = "frmProductBrwUC";
                    pnlBody.Children.Add(frmProductBrwUC);
                }

                HideAllBrowseForm("frmProductBrwUC");
                frmProductBrwUC.ParentForm = this;
                frmProductBrwUC.PopulateProductStatus();
                frmProductBrwUC.IsPOS = false;
                frmProductBrwUC.FetchData(false, frmProductBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_Products;
            }

            if (clickitem == "Services")
            {

                if (frm_ServiceBrwUC == null)
                {
                    frm_ServiceBrwUC = new POSSection.frm_ServiceBrwUC();
                    frm_ServiceBrwUC.Name = "frm_ServiceBrwUC";
                    pnlBody.Children.Add(frm_ServiceBrwUC);
                }

                HideAllBrowseForm("frm_ServiceBrwUC");
                frm_ServiceBrwUC.ParentForm = this;
                frm_ServiceBrwUC.PopulateServiceStatus();
                frm_ServiceBrwUC.IsPOS = false;
                frm_ServiceBrwUC.FetchData(false, frm_ServiceBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_Services;
            }

            if (clickitem == "Families")
            {
                if (frm_BrandBrwUC == null)
                {
                    frm_BrandBrwUC = new POSSection.frm_BrandBrwUC();
                    frm_BrandBrwUC.Name = "frm_BrandBrwUC";
                    pnlBody.Children.Add(frm_BrandBrwUC);
                }

                HideAllBrowseForm("frm_BrandBrwUC");
                frm_BrandBrwUC.ParentForm = this;
                frm_BrandBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Families;
            }

            if (clickitem == "Departments")
            {
                if (frm_DepartmentBrwUC == null)
                {
                    frm_DepartmentBrwUC = new POSSection.frm_DepartmentBrwUC();
                    frm_DepartmentBrwUC.Name = "frm_DepartmentBrwUC";
                    pnlBody.Children.Add(frm_DepartmentBrwUC);
                }

                HideAllBrowseForm("frm_DepartmentBrwUC");
                frm_DepartmentBrwUC.ParentForm = this;
                frm_DepartmentBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Departments;
            }

            if (clickitem == "POS Screen Categories")
            {

                if (frm_CategoryBrwUC == null)
                {
                    frm_CategoryBrwUC = new POSSection.frm_CategoryBrwUC();
                    frm_CategoryBrwUC.Name = "frm_CategoryBrwUC";
                    pnlBody.Children.Add(frm_CategoryBrwUC);
                }


                HideAllBrowseForm("frm_CategoryBrwUC");
                frm_CategoryBrwUC.ParentForm = this;
                frm_CategoryBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Categories;
                frm_CategoryBrwUC.EnableDisableButton();
            }

            if (clickitem == "Stock Journal")
            {
                if (frm_JournalBrwUC == null)
                {
                    frm_JournalBrwUC = new POSSection.frm_JournalBrwUC();
                    frm_JournalBrwUC.Name = "frm_JournalBrwUC";
                    pnlBody.Children.Add(frm_JournalBrwUC);
                }

                HideAllBrowseForm("frm_JournalBrwUC");
                frm_JournalBrwUC.ParentForm = this;
                frm_JournalBrwUC.Flag = false;
                frm_JournalBrwUC.FDate.EditValue = DateTime.Today.Date;
                frm_JournalBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_JournalBrwUC.PopulateProduct();
                frm_JournalBrwUC.PopulateAction();
                frm_JournalBrwUC.Flag = true;
                lbHeading.Text = Properties.Resources.List_Stock_Journal;

                frm_JournalBrwUC.FetchData(GeneralFunctions.fnDate(frm_JournalBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_JournalBrwUC.TDate.EditValue.ToString()), frm_JournalBrwUC.cmbAction.EditValue.ToString(),
                                         GeneralFunctions.fnInt32(frm_JournalBrwUC.cmbItem.EditValue));
            }

            if (clickitem == "Inventory Adjustment")
            {
                if (frm_StocktakeBrwUC == null)
                {
                    frm_StocktakeBrwUC = new POSSection.frm_StocktakeBrwUC();
                    frm_StocktakeBrwUC.Name = "frm_StocktakeBrwUC";
                    pnlBody.Children.Add(frm_StocktakeBrwUC);
                }

                HideAllBrowseForm("frm_StocktakeBrwUC");
                frm_StocktakeBrwUC.ParentForm = this;
                lbHeading.Text = Properties.Resources.List_Inventory_Adjustment;
                frm_StocktakeBrwUC.Flag = false;
                frm_StocktakeBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_StocktakeBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_StocktakeBrwUC.Flag = true;
                frm_StocktakeBrwUC.FetchData(GeneralFunctions.fnDate(frm_StocktakeBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_StocktakeBrwUC.TDate.EditValue.ToString()));
            }

            if (clickitem == "Import Bookers")
            {
                //  MessageBoxLoadingWindow MBLW = new MessageBoxLoadingWindow(true);
                //  MBLW.SetText("Bookers Import", "Initializing Import");
                //  MBLW.Show(); 
               // MBLW.Close();

                  
                 var MBLW = new MessageBoxLoadingWindow(true);
                 MBLW.SetText("Booker Import", "Importing Products Data");
                MBLW.Show();

                PosDataObject.BookerService bookerService = new PosDataObject.BookerService();
                bool bookerImportResult = bookerService.StartProcess();
                MBLW.Close();
                
                if (bookerImportResult)
                    new MessageBoxWindow().Show("Bookers Import Completed Successfully", "Booker Import", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    new MessageBoxWindow().Show("Bookers Import Failed", "Booker Import", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            if (clickitem == "Purchase Order")
            {
                if (frm_POBrwUC == null)
                {
                    frm_POBrwUC = new POSSection.frm_POBrwUC();
                    frm_POBrwUC.Name = "frm_POBrwUC";
                    pnlBody.Children.Add(frm_POBrwUC);
                }

                HideAllBrowseForm("frm_POBrwUC");
                frm_POBrwUC.ParentForm = this;
                frm_POBrwUC.Flag = false;
                frm_POBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_POBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_POBrwUC.PopulateVendor();
                frm_POBrwUC.Flag = true;
                frm_POBrwUC.SetDecimalPlace();
                frm_POBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_POBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_POBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.List_of_Purchase_Orders;
            }

            if (clickitem == "Receiving")
            {
                if (frm_ReceivingBrwUC == null)
                {
                    frm_ReceivingBrwUC = new frm_ReceivingBrwUC();
                    frm_ReceivingBrwUC.Name = "frm_ReceivingBrwUC";
                    pnlBody.Children.Add(frm_ReceivingBrwUC);
                }

                HideAllBrowseForm("frm_ReceivingBrwUC");
                frm_ReceivingBrwUC.ParentForm = this;
                frm_ReceivingBrwUC.Flag = false;
                frm_ReceivingBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_ReceivingBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_ReceivingBrwUC.PopulateVendor();
                frm_ReceivingBrwUC.Flag = true;
                frm_ReceivingBrwUC.SetDecimalPlace();
                frm_ReceivingBrwUC.FetchData(0, GeneralFunctions.fnDate(frm_ReceivingBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_ReceivingBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.Receiving_Items;
            }

            if (clickitem == "Transfer")
            {
                if (frm_TransferBrwUC == null)
                {
                    frm_TransferBrwUC = new frm_TransferBrwUC();
                    frm_TransferBrwUC.Name = "frm_TransferBrwUC";
                    pnlBody.Children.Add(frm_TransferBrwUC);
                }

                HideAllBrowseForm("frm_TransferBrwUC");
                frm_TransferBrwUC.ParentForm = this;
                frm_TransferBrwUC.Flag = false;
                frm_TransferBrwUC.FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                frm_TransferBrwUC.TDate.EditValue = DateTime.Today.Date;
                frm_TransferBrwUC.Flag = true;
                frm_TransferBrwUC.SetDecimalPlace();
                frm_TransferBrwUC.FetchData(GeneralFunctions.fnDate(frm_TransferBrwUC.FDate.EditValue.ToString()), GeneralFunctions.fnDate(frm_TransferBrwUC.TDate.EditValue.ToString()));
                lbHeading.Text = Properties.Resources.Transfer_Items;
            }

            if (clickitem == "Vendors")
            {
                if (frm_VendorBrwUC == null)
                {
                    frm_VendorBrwUC = new frm_VendorBrwUC();
                    frm_VendorBrwUC.Name = "frm_VendorBrwUC";
                    pnlBody.Children.Add(frm_VendorBrwUC);
                }

                HideAllBrowseForm("frm_VendorBrwUC");
                frm_VendorBrwUC.ParentForm = this;
                frm_VendorBrwUC.FetchData();
                lbHeading.Text = "List of Vendors";
            }

            if (clickitem == "Print Labels")
            {

                if (frm_PrintLabelBrwUC == null)
                {
                    frm_PrintLabelBrwUC = new Administrator.frm_PrintLabelBrwUC();
                    frm_PrintLabelBrwUC.Name = "frm_PrintLabelBrwUC";
                    pnlBody.Children.Add(frm_PrintLabelBrwUC);
                }

                HideAllBrowseForm("frm_PrintLabelBrwUC");
                frm_PrintLabelBrwUC.ParentForm = this;
                frm_PrintLabelBrwUC.FetchData();
                lbHeading.Text = "Print Labels";
            }


            if (clickitem == "Products Label Printing")
            {
                if (frm_ProductsLabelPrinting == null)
                {
                    frm_ProductsLabelPrinting = new Administrator.frm_ProductsLabelPrinting();
                    frm_ProductsLabelPrinting.Name = "frm_ProductsLabelPrinting";
                    pnlBody.Children.Add(frm_ProductsLabelPrinting);
                }

                HideAllBrowseForm("frm_ProductsLabelPrinting");
                frm_ProductsLabelPrinting.ParentForm = this;
                //frm_PrintLabelBrwUC.FetchData();
                lbHeading.Text = "Products Label Printing";
            }

            if (clickitem == "Zip Codes")
            {
                if (frmZipCodeBrwUC == null)
                {
                    frmZipCodeBrwUC = new POSSection.frmZipCodeBrwUC();
                    frmZipCodeBrwUC.Name = "frmZipCodeBrwUC";
                    pnlBody.Children.Add(frmZipCodeBrwUC);
                }

                HideAllBrowseForm("frmZipCodeBrwUC");
                frmZipCodeBrwUC.ParentForm = this;
                frmZipCodeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Zipcodes;
            }

            if (clickitem == "Sale Price")
            {
                if (frm_SalePriceBrwUC == null)
                {
                    frm_SalePriceBrwUC = new POSSection.frm_SalePriceBrwUC();
                    frm_SalePriceBrwUC.Name = "frm_SalePriceBrwUC";
                    pnlBody.Children.Add(frm_SalePriceBrwUC);
                }

                HideAllBrowseForm("frm_SalePriceBrwUC");
                frm_SalePriceBrwUC.ParentForm = this;
                frm_SalePriceBrwUC.PopulateSaleBatchFilters();
                frm_SalePriceBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Sale_Price;
            }

            if (clickitem == "Future Price")
            {
                if (frm_FuturePriceBrwUC == null)
                {
                    frm_FuturePriceBrwUC = new POSSection.frm_FuturePriceBrwUC();
                    frm_FuturePriceBrwUC.Name = "frm_FuturePriceBrwUC";
                    pnlBody.Children.Add(frm_FuturePriceBrwUC);
                }

                HideAllBrowseForm("frm_FuturePriceBrwUC");
                frm_FuturePriceBrwUC.ParentForm = this;
                frm_FuturePriceBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_Future_Price;
            }

            if (clickitem == "Discounts")
            {
                if (frm_DiscountBrwUC == null)
                {
                    frm_DiscountBrwUC = new frm_DiscountBrwUC();
                    frm_DiscountBrwUC.Name = "frm_DiscountBrwUC";
                    pnlBody.Children.Add(frm_DiscountBrwUC);
                }

                HideAllBrowseForm("frm_DiscountBrwUC");
                frm_DiscountBrwUC.ParentForm = this;
                frm_DiscountBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Discounts;
            }

            if (clickitem == "Buy 'n Get Free")
            {
                if (frm_BuynGetFreeBrwUC == null)
                {
                    frm_BuynGetFreeBrwUC = new frm_BuynGetFreeBrwUC();
                    frm_BuynGetFreeBrwUC.Name = "frm_BuynGetFreeBrwUC";
                    pnlBody.Children.Add(frm_BuynGetFreeBrwUC);
                }

                HideAllBrowseForm("frm_BuynGetFreeBrwUC");
                frm_BuynGetFreeBrwUC.ParentForm = this;
                frm_BuynGetFreeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Buy_n_Get_Free_items;
            }

            if (clickitem == "Mix and Match")
            {
                if (frm_Mix_n_MatchBrwUC == null)
                {
                    frm_Mix_n_MatchBrwUC = new frm_Mix_n_MatchBrwUC();
                    frm_Mix_n_MatchBrwUC.Name = "frm_Mix_n_MatchBrwUC";
                    pnlBody.Children.Add(frm_Mix_n_MatchBrwUC);
                }

                HideAllBrowseForm("frm_Mix_n_MatchBrwUC");

                frm_Mix_n_MatchBrwUC.ParentForm = this;
                frm_Mix_n_MatchBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Mix_n_Match_items;
            }

            if (clickitem == "Break Packs")
            {
                if (frm_BreakPackBrwUC == null)
                {
                    frm_BreakPackBrwUC = new frm_BreakPackBrwUC();
                    frm_BreakPackBrwUC.Name = "frm_BreakPackBrwUC";
                    pnlBody.Children.Add(frm_BreakPackBrwUC);
                }

                HideAllBrowseForm("frm_BreakPackBrwUC");
                frm_BreakPackBrwUC.ParentForm = this;
                frm_BreakPackBrwUC.PopulateBreakPackStatus();
                frm_BreakPackBrwUC.IsPOS = false;
                frm_BreakPackBrwUC.FetchData(false, frm_BreakPackBrwUC.cmbFilter.Text);
                lbHeading.Text = Properties.Resources.List_of_Break_Packs;
            }

            if (clickitem == "Employees")
            {
                if (frm_EmployeeBrwUC == null)
                {
                    frm_EmployeeBrwUC = new frm_EmployeeBrwUC();
                    frm_EmployeeBrwUC.Name = "frm_EmployeeBrwUC";
                    pnlBody.Children.Add(frm_EmployeeBrwUC);
                }

                HideAllBrowseForm("frm_EmployeeBrwUC");
                frm_EmployeeBrwUC.ParentForm = this;
                frm_EmployeeBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Employees;
            }

            if (clickitem == "Printer Templates")
            {
                if (frm_PrinterTemplateBrwUC == null)
                {
                    frm_PrinterTemplateBrwUC = new Administrator.frm_PrinterTemplateBrwUC();
                    frm_PrinterTemplateBrwUC.Name = "frm_PrinterTemplateBrwUC";
                    pnlBody.Children.Add(frm_PrinterTemplateBrwUC);
                }

                HideAllBrowseForm("frm_PrinterTemplateBrwUC");
                frm_PrinterTemplateBrwUC.ParentForm = this;
                frm_PrinterTemplateBrwUC.FetchData();
                lbHeading.Text = "List of Printer Templates";
            }

            if (clickitem == "Shifts")
            {
                if (frmShiftBrwUC == null)
                {
                    frmShiftBrwUC = new frmShiftBrwUC();
                    frmShiftBrwUC.Name = "frmShiftBrwUC";
                    pnlBody.Children.Add(frmShiftBrwUC);
                }

                HideAllBrowseForm("frmShiftBrwUC");
                frmShiftBrwUC.ParentForm = this;
                frmShiftBrwUC.FetchGridData();
                lbHeading.Text = Properties.Resources.List_of_Shifts;
            }

            if (clickitem == "Holidays")
            {
                if (frmHolidayBrwUC == null)
                {
                    frmHolidayBrwUC = new frmHolidayBrwUC();
                    frmHolidayBrwUC.Name = "frmHolidayBrwUC";
                    pnlBody.Children.Add(frmHolidayBrwUC);
                }

                HideAllBrowseForm("frmHolidayBrwUC");
                frmHolidayBrwUC.ParentForm = this;
                frmHolidayBrwUC.Year = DateTime.Today.Year;
                frmHolidayBrwUC.PopulateYear();
                lbHeading.Text = Properties.Resources.List_of_Holidays;
            }

            if (clickitem == "G/L Account")
            {
                if (frm_GLBrwUC == null)
                {
                    frm_GLBrwUC = new Administrator.frm_GLBrwUC();
                    frm_GLBrwUC.Name = "frm_GLBrwUC";
                    pnlBody.Children.Add(frm_GLBrwUC);
                }

                HideAllBrowseForm("frm_GLBrwUC");
                frm_GLBrwUC.ParentForm = this;
                frm_GLBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_GL_Accounts;
            }

            if (clickitem == "Tax")
            {
                if (frm_TaxBrwUC == null)
                {
                    frm_TaxBrwUC = new Administrator.frm_TaxBrwUC();
                    frm_TaxBrwUC.Name = "frm_TaxBrwUC";
                    pnlBody.Children.Add(frm_TaxBrwUC);
                }

                HideAllBrowseForm("frm_TaxBrwUC");
                frm_TaxBrwUC.ParentForm = this;
                frm_TaxBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.List_of_Taxes;
            }

            if (clickitem == "Tender Types")
            {
                if (frm_TenderTypesBrwUC == null)
                {
                    frm_TenderTypesBrwUC = new Administrator.frm_TenderTypesBrwUC();
                    frm_TenderTypesBrwUC.Name = "frm_TenderTypesBrwUC";
                    pnlBody.Children.Add(frm_TenderTypesBrwUC);
                }

                HideAllBrowseForm("frm_TenderTypesBrwUC");
                frm_TenderTypesBrwUC.ParentForm = this;
                frm_TenderTypesBrwUC.FetchData();
                lbHeading.Text = "List of Tender Types";
                frm_TenderTypesBrwUC.EnableDisableButton();
            }

            if (clickitem == "Security")
            {
                if (frm_SecurityGroupBrwUC == null)
                {
                    frm_SecurityGroupBrwUC = new Administrator.frm_SecurityGroupBrwUC();
                    frm_SecurityGroupBrwUC.Name = "frm_SecurityGroupBrwUC";
                    pnlBody.Children.Add(frm_SecurityGroupBrwUC);
                }

                HideAllBrowseForm("frm_SecurityGroupBrwUC");
                frm_SecurityGroupBrwUC.ParentForm = this;
                frm_SecurityGroupBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Security_Profiles;
            }

            if (clickitem == "Fees & Charges")
            {
                if (frm_FeesBrwUC == null)
                {
                    frm_FeesBrwUC = new Administrator.frm_FeesBrwUC();
                    frm_FeesBrwUC.Name = "frm_FeesBrwUC";
                    pnlBody.Children.Add(frm_FeesBrwUC);
                }


                HideAllBrowseForm("frm_FeesBrwUC");
                frm_FeesBrwUC.ParentForm = this;
                frm_FeesBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Fees___Charges;
            }

            if (clickitem == "Price Adjustment")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Discounts";
                lbHeading.Text = Properties.Resources.Price_Adjustment;

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                frm_PriceAdjustmentDlg frm_ = new frm_PriceAdjustmentDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (clickitem == "Reorder Report")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Ordering";
                lbHeading.Text = "Reorder Reports";

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Report.frm_ReorderReportDlg frm_ = new Report.frm_ReorderReportDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }



            if (clickitem == "Registration")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Administrator";
                lbHeading.Text = Properties.Resources.Registration;

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                frmRegistrationDlg frm_RegistrationDlg = new frmRegistrationDlg();
                try
                {
                    frm_RegistrationDlg.FirstTimeCall = false;
                    frm_RegistrationDlg.ShowDialog();
                    if (frm_RegistrationDlg.DialogResult == true)
                    {
                        if (frm_RegistrationDlg.Registered)
                        {
                            //StatusItem4.Caption = " " + Translation.SetMultilingualTextInCodes("Registered to :", "frmMain_Registeredto") + " " + Settings.RegCompanyName;
                            DocMessage.MsgInformation("Successful Registration, Terminating Application...");
                            Application.Current.Shutdown();
                        }
                    }
                }
                finally
                {
                    frm_RegistrationDlg.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (clickitem == "General Settings")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Administrator";
                lbHeading.Text = Properties.Resources.General_Settings;

                if (IsPOSInstalled())
                {
                    CheckAccessForGeneralSettings("31z1");
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                    Administrator.frm_GeneralSetupDlg frm_GeneralSetupDlg = new Administrator.frm_GeneralSetupDlg();
                    try
                    {
                        frm_GeneralSetupDlg.CallFromAdmin = true;
                        frm_GeneralSetupDlg.bLoad = false;
                        frm_GeneralSetupDlg.MainF = this;
                        frm_GeneralSetupDlg.FunctionOrderChangeAccess = blFunctionOrderChangeAccess;
                        frm_GeneralSetupDlg.FunctionBtnAccess = blFunctionBtnAccess;
                        frm_GeneralSetupDlg.ShowDialog();
                    }
                    finally
                    {

                        string ModulesRegistered = GeneralFunctions.RegisteredModules();

                        if (!ModulesRegistered.Contains("SCALE"))
                        {

                        }
                    }
                    if (frm_GeneralSetupDlg.ThemeCHanged)
                    {
                        SetItemsPanelFromSelectedGroupSpl(mnuSettings);
                    }
                    frm_GeneralSetupDlg.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    DocMessage.MsgInformation("You must install POS for .Net");
                }
            }

            if (clickitem == "XEPOS Host")
            {
                HideAllBrowseForm("frm_DummyBrwUC");
                frm_DummyBrwUC.ParentForm = this;
                frm_DummyBrwUC.MenuHeader = "Administrator";
                lbHeading.Text = Properties.Resources.XEPOS_Host_Setup;

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                Administrator.frm_GwareHostSetupDlg frm_ = new Administrator.frm_GwareHostSetupDlg();
                try
                {
                    frm_.ShowDialog();
                }
                finally
                {
                    frm_.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }

            if (clickitem == "Host Customers")
            {
                if (frm_ImportCustomerBrwUC == null)
                {
                    frm_ImportCustomerBrwUC = new Administrator.frm_ImportCustomerBrwUC();
                    frm_ImportCustomerBrwUC.Name = "frm_ImportCustomerBrwUC";
                    pnlBody.Children.Add(frm_ImportCustomerBrwUC);
                }

                HideAllBrowseForm("frm_ImportCustomerBrwUC");
                frm_ImportCustomerBrwUC.ParentForm = this;
                frm_ImportCustomerBrwUC.storecd = Settings.StoreCode;
                frm_ImportCustomerBrwUC.PopulateStore();
                frm_ImportCustomerBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Import_Customers;
            }

            if (clickitem == "Inventory")
            {
                if (frm_InventoryBrwUC == null)
                {
                    frm_InventoryBrwUC = new Administrator.frm_InventoryBrwUC();
                    frm_InventoryBrwUC.Name = "frm_InventoryBrwUC";
                    pnlBody.Children.Add(frm_InventoryBrwUC);
                }

                HideAllBrowseForm("frm_InventoryBrwUC");
                frm_InventoryBrwUC.ParentForm = this;
                frm_InventoryBrwUC.storecd = Settings.StoreCode;
                frm_InventoryBrwUC.PopulateStore();
                frm_InventoryBrwUC.PopulateSKU();
                frm_InventoryBrwUC.FetchData();
                lbHeading.Text = Properties.Resources.Inventory_Status;
            }

            if (clickitem == "Gift Certificates")
            {
                if (frm_GiftCertBrwUC == null)
                {
                    frm_GiftCertBrwUC = new Administrator.frm_GiftCertBrwUC();
                    frm_GiftCertBrwUC.Name = "frm_GiftCertBrwUC";
                    pnlBody.Children.Add(frm_GiftCertBrwUC);
                }

                HideAllBrowseForm("frm_GiftCertBrwUC");
                frm_GiftCertBrwUC.ParentForm = this;
                frm_GiftCertBrwUC.FetchGC();
                lbHeading.Text = Properties.Resources.Gift_Certificates;
            }
        }

        private string IsUpdateAvailable(ref bool boolconnect, ref bool fileexists)
        {
            try
            {
                string boolResult = "";
                string strresult = ""; string strCurrentVersion = "";

                long versiontxtfile = GetFtpFileSizeVersionText(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));
                long msiFile = GetFtpFileSizeMsi(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));

                if ((versiontxtfile.ToString() == "0") || (msiFile.ToString() == "0"))
                {

                }
                else
                {
                    fileexists = true;
                    using (var client = new WebClient())
                    {
                        //strresult = client.DownloadString("http://www.photosurfer.com/PowerSourceUpdate/version.txt");



                        client.Credentials = new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@");

                        strresult = client.DownloadString("ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt");
                    }

                    if (strresult != "")
                    {
                        string NewVersion = strresult;
                        strCurrentVersion = GeneralFunctions.VersionInfoForUpdate();
                        strresult = strresult.Replace(".", "");
                        strCurrentVersion = strCurrentVersion.Replace(".", "");
                        if (Convert.ToInt16(strCurrentVersion) < Convert.ToInt16(strresult))
                        {
                            boolResult = NewVersion;
                        }
                    }

                   
                }
                return boolResult;
            }
            catch (Exception ex)
            {
                boolconnect = false;
                DocMessage.MsgError("Can not Connect to Server");
                string errmsg = ex.Message;
                return "";
            }

        }

        private long GetFtpFileSizeVersionText(Uri requestUri, NetworkCredential networkCredential)
        {

            var ftpWebRequest = GetFtpWebRequest(requestUri, networkCredential, WebRequestMethods.Ftp.GetFileSize);

            try { return ((FtpWebResponse)ftpWebRequest.GetResponse()).ContentLength; } //Incase of success it'll return the File Size.
            catch (Exception) { return default(long); } //Incase of fail it'll return default value to check it later.
        }

        private long GetFtpFileSizeMsi(Uri requestUri, NetworkCredential networkCredential)
        {

            var ftpWebRequest = GetFtpWebRequest(requestUri, networkCredential, WebRequestMethods.Ftp.GetFileSize);

            try { return ((FtpWebResponse)ftpWebRequest.GetResponse()).ContentLength; } //Incase of success it'll return the File Size.
            catch (Exception) { return default(long); } //Incase of fail it'll return default value to check it later.
        }

        private FtpWebRequest GetFtpWebRequest(Uri requestUri, NetworkCredential networkCredential, string method = null)
        {
            var ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUri); //Create FtpWebRequest with given Request Uri.
            ftpWebRequest.Credentials = networkCredential; //Set the Credentials of current FtpWebRequest.

            if (!string.IsNullOrEmpty(method))
                ftpWebRequest.Method = method; //Set the Method of FtpWebRequest incase it has a value.
            return ftpWebRequest; //Return the configured FtpWebRequest.
        }
    }

    public class ExpandButtonHelper
    {


        public static bool? GetHideExpandButton(DependencyObject obj)
        {
            return (bool?)obj.GetValue(HideExpandButtonProperty);
        }

        public static void SetHideExpandButton(DependencyObject obj, bool? value)
        {
            obj.SetValue(HideExpandButtonProperty, value);
        }

        // Using a DependencyProperty as the backing store for HideExpandButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideExpandButtonProperty =
            DependencyProperty.RegisterAttached("HideExpandButton", typeof(bool?), typeof(ExpandButtonHelper), new PropertyMetadata(null, (d, e) =>
            {
                if ((bool?)e.NewValue == true)
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                    {
                        var control = d as NavBarGroupControl;
                        var button = LayoutTreeHelper.GetVisualChildren(control).OfType<ExplorerBarExpandButton>().FirstOrDefault();
                        button.Template = null;
                    }));
                }
            }));


    }
}
