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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;
using TaskScheduler;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Threading;
using System.IO;

namespace OfflineRetailV2.UserControls.Administrator
{

    public static class ExtensionMethods
    {
        private static readonly System.Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
    /// <summary>
    /// Interaction logic for frm_GwareHostSetupDlg.xaml
    /// </summary>
    public partial class frm_GwareHostSetupDlg : Window
    {
        private string logF = "";
        private string logP = "";

        private string logCF = "";
        private string logCP = "";

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_GwareHostSetupDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private bool boolControlChanged;

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void ChkCentral_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (chkCentral.IsChecked == true)
            {
                gcco1.IsEnabled = true;
                gcco2.IsEnabled = true;
                gcco3.IsEnabled = true;
                gcco4.IsEnabled = true;
            }
            else
            {
                gcco1.IsEnabled = false;
                gcco2.IsEnabled = false;
                gcco3.IsEnabled = false;
                gcco4.IsEnabled = false;
            }
        }

        private bool SetCloudConn()
        {
            string strconn = Settings.XEPOSCloudConnectionParameter;
            String ConnStr = "";
            System.Data.SqlClient.SqlConnection Conn;
            try
            {
                ConnStr = strconn;
                Conn = new System.Data.SqlClient.SqlConnection(ConnStr);
                SystemVariables.rmConn = Conn;
                Conn.Open();
                Conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AddActionInGwareHost(string pStoreCode, string pActionDetails)
        {
            PosDataObject.Central objCentral = new PosDataObject.Central();
            objCentral.Connection = SystemVariables.rmConn;
            objCentral.AddActionInWebOffice(pStoreCode, pActionDetails, Settings.CentralCustomerID);
        }

        private void BtnClientLogout_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation(Properties.Resources.Disconnet_with_XEPOS_account_will_disable_Host_operation  + " " + Properties.Resources.Do_you_want_to_continue_) == MessageBoxResult.Yes)
            {
                Cursor = Cursors.Wait;
                WriteToCloudFile("");
                WriteToCloudFile("Action: Logout from XEPOS Account" + " : start");
                WriteToCloudFile("Version: " + Settings.SoftwareVersion);
                try
                {

                    if (SetCloudConn())
                    {
                        PosDataObject.Setup obj = new PosDataObject.Setup();
                        obj.Connection = SystemVariables.Conn;
                        obj.UpdateCentralCustomerID(0);

                        obj.UpdateCentralExportImportFlag("N");

                        Settings.LoadSettingsVariables();
                        if (Settings.DefautInternational == "N")
                            GeneralFunctions.SetCurrencyNDateformat();

                        DocMessage.MsgInformation(Properties.Resources.Customer_disconnected_successfully_from_XEPOS_Account);

                        txtClientCode.Text = txtClientPassword.Text = "";
                        txtClientCode.IsEnabled = txtClientPassword.IsEnabled = true;
                        btnClientLogout.Visibility = Visibility.Hidden;
                        btnClientLogin.IsEnabled = true;
                        chkCentral.IsEnabled = true;
                        chkCentral.IsChecked = false;
                        WriteToCloudFile("Customer disconnected successfully from XEPOS Account");
                    }
                    else
                    {
                        WriteToCloudFile("Host connection fails");
                        DocMessage.MsgInformation(Properties.Resources.Cannot_connect_to_XEPOS_Host_Please_check_the_parameters);
                    }
                }
                finally
                {
                    WriteToCloudFile("Action: Logout from XEPOS Account" + " : end");
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void BtnClientLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtClientCode.Text.Trim() == "")
            {

                DocMessage.MsgEnter(Properties.Resources.Customer_Code);
                GeneralFunctions.SetFocus(txtClientCode);
                return;
            }

            if (txtClientPassword.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Password);
                GeneralFunctions.SetFocus(txtClientPassword);
                return;
            }

            Cursor = Cursors.Wait;
            WriteToCloudFile("");
            WriteToCloudFile("Action: Login to XEPOS Account" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);

            try
            {

                

                if (SetCloudConn())
                {

                    PosDataObject.Central ochk = new PosDataObject.Central();
                    ochk.Connection = SystemVariables.rmConn;

                    int pReturnId = ochk.GetCustomerInformationFromWeb(txtClientCode.Text.Trim(), txtClientPassword.Text.Trim());

                    if (pReturnId > 0)
                    {
                        PosDataObject.Setup obj = new PosDataObject.Setup();
                        obj.Connection = SystemVariables.Conn;
                        obj.UpdateCentralCustomerID(pReturnId);

                        obj.UpdateCentralExportImportFlag("Y");

                        Settings.LoadSettingsVariables();
                        if (Settings.DefautInternational == "N")
                            GeneralFunctions.SetCurrencyNDateformat();

                        DocMessage.MsgInformation(Properties.Resources.Customer_logged_successfully_to_XEPOS_Account);

                        txtClientCode.IsEnabled = txtClientPassword.IsEnabled = false;
                        btnClientLogout.Visibility = Visibility.Visible;
                        btnClientLogin.IsEnabled = false;
                        chkCentral.IsEnabled = false;


                        bool f1 = false;
                        PosDataObject.Central ochkyyy = new PosDataObject.Central();
                        ochkyyy.Connection = SystemVariables.rmConn;
                        f1 = ochkyyy.IfExistStoreInWebOffice(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                        WriteToCloudFile("Check if Store Code exists in Host");
                        if (f1)
                        {
                            WriteToCloudFile("Store Code exists in Host");
                            if (GeneralFunctions.fnInt32(Settings.strStoreHostId) == 0) // Update store with HostID
                            {
                                WriteToCloudFile("try to update store with Host Reference");
                                PosDataObject.Central ochk110 = new PosDataObject.Central();
                                ochk110.Connection = SystemVariables.rmConn;
                                int hostId = ochk110.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                                if (hostId > 0)
                                {
                                    PosDataObject.Central ochk111 = new PosDataObject.Central();
                                    ochk111.Connection = SystemVariables.Conn;
                                    string retval = ochk111.UpdateStoreHostID(hostId);
                                    WriteToCloudFile("update store with Host Reference");
                                }
                            }
                            else
                            {
                                WriteToCloudFile("update store data in Host");
                                PosDataObject.Central ochk112 = new PosDataObject.Central();
                                ochk112.Connection = SystemVariables.rmConn;
                                string retvalupdt = ochk112.UpdateStoreInWebOffice(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, GeneralFunctions.fnInt32(Settings.strStoreHostId));
                            }
                        }
                        else
                        {
                            WriteToCloudFile("Store Code not exists in Host");
                            if (GeneralFunctions.fnInt32(Settings.strStoreHostId) == 0)
                            {
                                WriteToCloudFile("Add Store in Host if Host Reference not found ");
                                PosDataObject.Central ochk115 = new PosDataObject.Central();
                                ochk115.Connection = SystemVariables.rmConn;
                                ochk115.InsertStoreInWebOfficeNew(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, Settings.strStoreId);
                            }
                            else
                            {
                                WriteToCloudFile("Update Store in Host if Host Reference found ");
                                PosDataObject.Central ochk112 = new PosDataObject.Central();
                                ochk112.Connection = SystemVariables.rmConn;
                                string retvalupdt = ochk112.UpdateStoreInWebOffice(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, GeneralFunctions.fnInt32(Settings.strStoreHostId));

                                PosDataObject.Central ochk110 = new PosDataObject.Central();
                                ochk110.Connection = SystemVariables.rmConn;
                                int hostId = ochk110.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                                if (hostId > 0)
                                {
                                    PosDataObject.Central ochk111 = new PosDataObject.Central();
                                    ochk111.Connection = SystemVariables.Conn;
                                    string retval = ochk111.UpdateStoreHostID(hostId);
                                }

                            }
                        }

                      

                        if (f1)
                        {
                            try
                            {
                                AddActionInGwareHost(txtStoreCode.Text.Trim(), Properties.Resources.XEPOS_Host_connection_checked.Replace("XEPOS", SystemVariables.BrandName));
                            }
                            catch
                            {
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        WriteToCloudFile("Customer not found in Host with the given credencial");
                        DocMessage.MsgInformation(Properties.Resources.Unsuccessfull_Login_to_XEPOS_Account);
                    }
                }
                else
                {
                    WriteToCloudFile("Host connection fails");
                    DocMessage.MsgInformation(Properties.Resources.Cannot_connect_to_XEPOS_Host_Please_check_the_parameters);
                }
            }
            catch(Exception ex)
            {
                WriteToCloudFile(ex.Message);
            }
            finally
            {
                WriteToCloudFile("Action: Login to XEPOS Account" + " : end");
                Cursor = Cursors.Arrow;
            }
        }


        private bool IsActiveCentralCustomer()
        {
            if (SetCloudConn())
            {
                PosDataObject.Central objCentral = new PosDataObject.Central();
                objCentral.Connection = SystemVariables.rmConn;
                return objCentral.IsActiveCustomer(Settings.CentralCustomerID);
            }
            else
            {
                return false;
            }

        }
        private bool ValidForExport()
        {
            if (Settings.CentralCustomerID == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Please_login_to_XEPOS_Account);
                GeneralFunctions.SetFocus(txtClientCode);
                return false;
            }

            if ((txtStoreCode.Text.Trim() == "") || (txtStoreName.Text.Trim() == ""))
            {
                DocMessage.MsgInformation(Properties.Resources.Store_Info_cannot_be_blank);
                GeneralFunctions.SetFocus(txtStoreCode);
                return false;
            }

            if (!IsActiveCentralCustomer())
            {
                DocMessage.MsgInformation(Properties.Resources.Please_login_with_a_valid_XEPOS_Account);
                GeneralFunctions.SetFocus(txtClientCode);
                return false;
            }

            return true;
        }

        private bool SetCloudConnForExport()
        {
            string strconn = Settings.XEPOSCloudConnectionParameter;
            String ConnStr = "";
            System.Data.SqlClient.SqlConnection Conn;
            try
            {
                ConnStr = strconn;
                Conn = new System.Data.SqlClient.SqlConnection(ConnStr);
                SystemVariables.rmConn = Conn;
                SystemVariables.rmConnectionString = ConnStr;
                Conn.Open();
                Conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckAndSetParameterBeforeTaskSchedulerActivity()
        {
            if (Settings.CentralCustomerID == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Please_login_to_XEPOS_Account);
                GeneralFunctions.SetFocus(txtClientCode);
                return false;
            }
            if ((txtStoreCode.Text.Trim() == "") || (txtStoreName.Text.Trim() == ""))
            {
                DocMessage.MsgInformation(Properties.Resources.Store_Info_cannot_be_blank);
                GeneralFunctions.SetFocus(txtStoreCode);
                return false;
            }



            int StoreExists = GeneralFunctions.GetRecordCount("CentralExportImport");
            string err = "";

            PosDataObject.Setup objsetup = new PosDataObject.Setup();
            objsetup.Connection = SystemVariables.Conn;
            if (StoreExists == 0)
            {
                try
                {
                    err = objsetup.AddStoreCodeForGwareHost(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    err = objsetup.UpdateStoreCodeForGwareHost(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());
                }
                catch
                {
                }
            }


            if (err == "")
            {
                Settings.StoreCode = txtStoreCode.Text;
                Settings.StoreName = txtStoreName.Text;
                Settings.CentralExportImport = chkCentral.IsChecked == true ? "Y" : "N";
                return true;
            }
            else
            {
                return false;
            }

        }

        private void RefreshExportText(string txt)
        {
            txtExportStatus.Text = "Export Existing Master Data to XEPOS Host - Start";
            this.txtExportStatus.Refresh();
            System.Threading.Thread.Sleep(2000);
        }

        private void BtnExportMasterData_Click(object sender, RoutedEventArgs e)
        {
            WriteToCloudFile("");
            WriteToCloudFile("Action: Export existing Master Data to XEPOS Host" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);
            txtExportStatus.Text = "";
            this.txtExportStatus.Refresh();
            if (!ValidForExport())
            {
                WriteToCloudFile("Host Connection fails");
                WriteToCloudFile("Action: Export existing Master Data to XEPOS Host" + " : end");
                return;
            }

           


            Cursor = Cursors.Wait;
            try
            {

                PosDataObject.Setup ost1 = new PosDataObject.Setup();
                ost1.Connection = SystemVariables.Conn;
                bool blstore = ost1.IsExistStoreCode();

                if (!blstore)
                {
                    PosDataObject.Setup ost2 = new PosDataObject.Setup();
                    ost2.Connection = SystemVariables.Conn;
                    string err_store2 = ost2.InsertStoreOnFirstTimeWebExport(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());

                    PosDataObject.Setup ost3 = new PosDataObject.Setup();
                    ost3.Connection = SystemVariables.Conn;
                    string err_store3 = ost3.UpdateCustomerOnFirstTimeWebExport(txtStoreCode.Text.Trim());

                    PosDataObject.Setup ost4 = new PosDataObject.Setup();
                    ost4.Connection = SystemVariables.Conn;
                    string err_store4 = ost4.UpdateCustomerAcRecvOnFirstTimeWebExport(txtStoreCode.Text.Trim());

                    PosDataObject.Setup ost5 = new PosDataObject.Setup();
                    ost5.Connection = SystemVariables.Conn;
                    string err_store5 = ost5.UpdateEmployeeOnFirstTimeWebExport(txtStoreCode.Text.Trim());

                    PosDataObject.Setup ost6 = new PosDataObject.Setup();
                    ost6.Connection = SystemVariables.Conn;
                    string err_store6 = ost6.UpdateGeneralMappingOnFirstTimeWebExport(txtStoreCode.Text.Trim());

                    PosDataObject.Setup ost7 = new PosDataObject.Setup();
                    ost7.Connection = SystemVariables.Conn;
                    string err_store7 = ost7.UpdateGiftCertOnFirstTimeWebExport(txtStoreCode.Text.Trim());

                    WriteToCloudFile("Add Store Record and initalize other table if Store record not saved in the store database");
                }

                DataTable dDept = new DataTable();
                DataTable dCat = new DataTable();
                DataTable dBrand = new DataTable();
                DataTable dVendor = new DataTable();
                DataTable dTax = new DataTable();


                // Scale Master Data
                DataTable dScale_Cat = new DataTable();
                DataTable dScale_Addr = new DataTable();
                DataTable dScale_LabelFormat = new DataTable();
                DataTable dScale_GraphicArt = new DataTable();

                DataTable dScale_SecondLabelType = new DataTable();
                DataTable dScale_LabelType = new DataTable();
                DataTable dScale_Graphic = new DataTable();

                DataTable dProduct = new DataTable();

                PosDataObject.Central oc1 = new PosDataObject.Central();
                oc1.Connection = SystemVariables.Conn;
                dDept = oc1.FetchDepartment();

                WriteToCloudFile("Prepare Product Department records for Export");

                PosDataObject.Central oc2 = new PosDataObject.Central();
                oc2.Connection = SystemVariables.Conn;
                dCat = oc2.FetchCategory();

                WriteToCloudFile("Prepare Product Category records for Export");

                PosDataObject.Central oc3 = new PosDataObject.Central();
                oc3.Connection = SystemVariables.Conn;
                dBrand = oc3.FetchBrand();

                WriteToCloudFile("Prepare Product Brand records for Export");

                PosDataObject.Central oc4 = new PosDataObject.Central();
                oc4.Connection = SystemVariables.Conn;
                dVendor = oc4.FetchVendor();

                WriteToCloudFile("Prepare Vendor records for Export");

                PosDataObject.Central oc5 = new PosDataObject.Central();
                oc5.Connection = SystemVariables.Conn;
                dTax = oc5.FetchActiveTax();

                WriteToCloudFile("Prepare Tax records for Export");

                PosDataObject.Central oc21 = new PosDataObject.Central();
                oc21.Connection = SystemVariables.Conn;
                dScale_Cat = oc21.FetchScaleCategories();

                PosDataObject.Central oc22 = new PosDataObject.Central();
                oc22.Connection = SystemVariables.Conn;
                dScale_Addr = oc22.FetchScaleAddresses();

                PosDataObject.Central oc23 = new PosDataObject.Central();
                oc23.Connection = SystemVariables.Conn;
                dScale_LabelFormat = oc23.FetchScaleLabelFormatRecord();

                PosDataObject.Central oc24 = new PosDataObject.Central();
                oc24.Connection = SystemVariables.Conn;
                dScale_GraphicArt = oc24.FetchScaleGraphicArtRecord();

                PosDataObject.Central oc25 = new PosDataObject.Central();
                oc25.Connection = SystemVariables.Conn;
                dScale_SecondLabelType = oc25.FetchScaleSecondLabelTypeRecord();

                PosDataObject.Central oc26 = new PosDataObject.Central();
                oc26.Connection = SystemVariables.Conn;
                dScale_LabelType = oc26.FetchScaleLabelTypeRecord();

                PosDataObject.Central oc27 = new PosDataObject.Central();
                oc27.Connection = SystemVariables.Conn;
                dScale_Graphic = oc27.FetchScaleGraphicsRecord();


                PosDataObject.Central oc6 = new PosDataObject.Central();
                oc6.Connection = SystemVariables.Conn;
                dProduct = oc6.FetchProduct();

                WriteToCloudFile("Prepare Product records for Export");

                PosDataObject.Central ochkxx = new PosDataObject.Central();
                ochkxx.Connection = SystemVariables.Conn;
                ochkxx.UpdateStoreForWebExport(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());

                Settings.LoadCentralStoreInfo();


                bool f1 = false;
                PosDataObject.Central ochkyyy = new PosDataObject.Central();
                ochkyyy.Connection = SystemVariables.rmConn;
                f1 = ochkyyy.IfExistStoreInWebOffice(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);

                WriteToCloudFile("Check if Store exists in Host");
                if (f1)
                {
                    WriteToCloudFile("Store exists in Host");
                    if (GeneralFunctions.fnInt32(Settings.strStoreHostId) == 0) // Update store with HostID
                    {
                        WriteToCloudFile("try to update store with Host Reference");
                        PosDataObject.Central ochk110 = new PosDataObject.Central();
                        ochk110.Connection = SystemVariables.rmConn;
                        int hostId = ochk110.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                        if (hostId > 0)
                        {
                            PosDataObject.Central ochk111 = new PosDataObject.Central();
                            ochk111.Connection = SystemVariables.Conn;
                            string retval = ochk111.UpdateStoreHostID(hostId);
                            WriteToCloudFile("update store with Host Reference");
                        }
                    }
                    else
                    {
                        WriteToCloudFile("update store info in Host");
                        PosDataObject.Central ochk112 = new PosDataObject.Central();
                        ochk112.Connection = SystemVariables.rmConn;
                        string retvalupdt = ochk112.UpdateStoreInWebOffice(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, GeneralFunctions.fnInt32(Settings.strStoreHostId));
                    }
                }
                else
                {
                    WriteToCloudFile("Store not exists in Host");

                    if (GeneralFunctions.fnInt32(Settings.strStoreHostId) == 0)
                    {
                        WriteToCloudFile("Add Store in Host Databse if store's Host Reference not found in Store Database");
                        PosDataObject.Central ochk115 = new PosDataObject.Central();
                        ochk115.Connection = SystemVariables.rmConn;
                        ochk115.InsertStoreInWebOfficeNew(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, Settings.strStoreId);


                        PosDataObject.Central ochk1101 = new PosDataObject.Central();
                        ochk1101.Connection = SystemVariables.rmConn;
                        int hostId = ochk1101.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                        if (hostId > 0)
                        {
                            PosDataObject.Central ochk1111 = new PosDataObject.Central();
                            ochk1111.Connection = SystemVariables.Conn;
                            string retval = ochk1111.UpdateStoreHostID(hostId);
                        }

                    }
                    else
                    {
                        WriteToCloudFile("Try to update Store info in Host Databse in Host Reference found in Store Database");
                        PosDataObject.Central ochk112 = new PosDataObject.Central();
                        ochk112.Connection = SystemVariables.rmConn;
                        string retvalupdt = ochk112.UpdateStoreInWebOffice(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol, GeneralFunctions.fnInt32(Settings.strStoreHostId));

                        PosDataObject.Central ochk110 = new PosDataObject.Central();
                        ochk110.Connection = SystemVariables.rmConn;
                        int hostId = ochk110.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                        if (hostId > 0)
                        {
                            PosDataObject.Central ochk111 = new PosDataObject.Central();
                            ochk111.Connection = SystemVariables.Conn;
                            string retval = ochk111.UpdateStoreHostID(hostId);
                        }

                    }
                }


                /*
                bool f1 = false;
                PosDataObject.Central ochk = new PosDataObject.Central();
                ochk.Connection = SystemVariables.rmConn;
                f1 = ochk.IfExistStoreInWebOffice(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                //if (!f1)
                //{
                    ochk.InsertStoreInWebOffice(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim(), 0, Settings.CentralCustomerID, SystemVariables.CurrencySymbol,Settings.strStoreId);
                //}

                */

                txtExportStatus.Text = "Export Existing Master Data to XEPOS Host - Start";
                this.txtExportStatus.Refresh();

                /*Dispatcher.BeginInvoke(new System.Action(() => {
                    RefreshExportText("Export Existing Master Data to XEPOS Host - Start");
                }), System.Windows.Threading.DispatcherPriority.Render);
                */

                try
                {
                    AddActionInGwareHost(txtStoreCode.Text.Trim(), "Export Existing Master Data to XEPOS Host - Start");
                }
                catch(Exception ex)
                {
                    txtExportStatus.Text = ex.Message;
                    this.txtExportStatus.Refresh();
                }

                PosDataObject.Central ochk1 = new PosDataObject.Central();
                ochk1.Connection = SystemVariables.rmConn;

                int expcnt = 0;
                if (dDept.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting product department: " + dDept.Rows.Count.ToString());
                }
                foreach (DataRow dr in dDept.Rows)
                {
                    expcnt++;
                    string pcode = dr["Code"].ToString();
                    string pdesc = dr["Desc"].ToString();
                    string pscaleF = dr["Scale"].ToString();

                    try
                    {
                        ochk1.InsertDepartmentInWebOffice(Settings.CentralCustomerID, pcode, pdesc, pscaleF, 0);


                        txtExportStatus.Text = "Export Dept: " + expcnt.ToString() + "/" + dDept.Rows.Count.ToString();
                        this.txtExportStatus.Refresh();

                        

                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                        System.Threading.Thread.Sleep(2000);
                    }
                }

                expcnt = 0;

                if (dBrand.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting product brand: " + dBrand.Rows.Count.ToString());
                }

                foreach (DataRow dr in dBrand.Rows)
                {
                    expcnt++;
                    string pcode = dr["Code"].ToString();
                    string pdesc = dr["Desc"].ToString();
                    try
                    {
                        ochk1.InsertBrandInWebOffice(Settings.CentralCustomerID, pcode, pdesc, 0);

                        txtExportStatus.Text = "Export Family: " + expcnt.ToString() + "/" + dBrand.Rows.Count.ToString();
                        this.txtExportStatus.Refresh();

                        
                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                        
                    }
                }


                expcnt = 0;

                if (dCat.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting product category: " + dCat.Rows.Count.ToString());
                }

                foreach (DataRow dr in dCat.Rows)
                {
                    expcnt++;

                    string pcode = dr["Code"].ToString();
                    string pdesc = dr["Desc"].ToString();
                    string pposscreenclr = dr["POSScreenColor"].ToString();
                    string pposbkgrd = dr["POSBackground"].ToString();
                    string pposscreensty = dr["POSScreenStyle"].ToString();
                    string pposfontty = dr["POSFontType"].ToString();
                    int pposfontsz = GeneralFunctions.fnInt32(dr["POSFontSize"].ToString());
                    string pposfontclr = dr["POSFontColor"].ToString();
                    string ppositemfontclr = dr["POSItemFontColor"].ToString();
                    string pbold = dr["IsBold"].ToString();
                    string pitalic = dr["IsItalics"].ToString();
                    int pmaxitemsforpos = GeneralFunctions.fnInt32(dr["MaxItemsforPOS"].ToString());

                    try
                    {
                        ochk1.InsertCategoryInWebOffice(Settings.CentralCustomerID, pcode, pdesc, 0, pposscreenclr, pposbkgrd, pposscreensty,
                                                                pposfontty, pposfontsz, pposfontclr, ppositemfontclr, pbold,
                                                                pitalic, 0, pmaxitemsforpos);

                        txtExportStatus.Text = "Export Category: " + expcnt.ToString() + "/" + dCat.Rows.Count.ToString();
                        this.txtExportStatus.Refresh();

                        

                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                        
                    }
                }


                expcnt = 0;

                if (dVendor.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting vendor: " + dVendor.Rows.Count.ToString());
                }

                foreach (DataRow dr in dVendor.Rows)
                {
                    expcnt++;

                    string pcode = dr["VendorID"].ToString();
                    string paccno = dr["AccountNo"].ToString();
                    string pname = dr["Name"].ToString();
                    string pcontact = dr["Contact"].ToString();
                    string padd1 = dr["Address1"].ToString();
                    string padd2 = dr["Address2"].ToString();
                    string pcity = dr["City"].ToString();
                    string pstate = dr["State"].ToString();
                    string pcountry = dr["Country"].ToString();
                    string pzip = dr["Zip"].ToString();
                    string pfax = dr["Fax"].ToString();
                    string pph = dr["Phone"].ToString();
                    string pmail = dr["EMail"].ToString();
                    string pnote = dr["Notes"].ToString();
                    double minamt = GeneralFunctions.fnDouble(dr["MinimumOrderAmount"].ToString());

                    try
                    {

                        ochk1.InsertVendorInWebOffice(Settings.CentralCustomerID, pcode, paccno, pname, pcontact, padd1, padd2, pcity,
                                                         pstate, pcountry, pzip, pfax, pph, pmail, pnote, minamt,
                                                         0);

                        
                        if (pzip != "")
                        {
                            bool f2 = false;
                            f2 = ochk1.IfExistZipInWebOffice(pzip, Settings.CentralCustomerID);
                            if (!f2) ochk1.InsertZipInWebOffice(Settings.CentralCustomerID, pzip, pcity, pstate, 0);
                        }


                        txtExportStatus.Text = "Export Vendor: " + expcnt.ToString() + "/" + dVendor.Rows.Count.ToString();
                        this.txtExportStatus.Refresh();

                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                    }

                }


                expcnt = 0;

                if (dTax.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting tax: " + dTax.Rows.Count.ToString());
                }

                foreach (DataRow dr in dTax.Rows)
                {

                    expcnt++;
                    int pid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    int ptaxtype = GeneralFunctions.fnInt32(dr["TaxType"].ToString());
                    double ptaxrate = GeneralFunctions.fnDouble(dr["TaxRate"].ToString());
                    string ptax = dr["TaxName"].ToString();


                    try
                    {
                        bool f = false;

                        f = ochk1.IfExistTaxInWebOffice(ptax, Settings.CentralCustomerID);
                        int rfID = 0;
                        if (!f)
                        {
                            ochk1.InsertTaxInWebOffice(Settings.CentralCustomerID, ptax, ptaxtype, ptaxrate, "Yes", 0, ref rfID);

                            

                            if (ptaxtype == 1)
                            {
                                PosDataObject.Central otemp = new PosDataObject.Central();
                                otemp.Connection = SystemVariables.Conn;
                                DataTable dBreak = otemp.FetchTaxBreakpoints(pid);
                                foreach (DataRow dr1 in dBreak.Rows)
                                {
                                    ochk1.InsertTaxBreakPointInWebOffice(Settings.CentralCustomerID, rfID, GeneralFunctions.fnDouble(dr1["BreakPoints"].ToString()),
                                        GeneralFunctions.fnDouble(dr1["Tax"].ToString()), 0);
                                }

                            }
                        }

                        txtExportStatus.Text = "Export Tax: " + expcnt.ToString() + "/" + dTax.Rows.Count.ToString();
                        this.txtExportStatus.Refresh();
                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                    }
                }

                /*
                foreach (DataRow dr in dScale_Cat.Rows)
                {
                    string pCat_ID = dr["Cat_ID"].ToString();
                    string pScaleCat_Name = dr["Name"].ToString();
                    string pScaleCat_Ref_Department_ID = dr["Dept_ID"].ToString();
                    string pScaleCat_POSBackground = dr["POSBackground"].ToString();
                    string pScaleCat_POSScreenStyle = dr["POSScreenStyle"].ToString();
                    string pScaleCat_POSScreenColor = dr["POSScreenColor"].ToString();
                    string pScaleCat_POSFontType = dr["POSFontType"].ToString();
                    int pScaleCat_POSFontSize = GeneralFunctions.fnInt32(dr["POSFontSize"].ToString());
                    string pScaleCat_POSFontColor = dr["POSFontColor"].ToString();
                    string pScaleCat_IsBold = dr["IsBold"].ToString();
                    string pScaleCat_IsItalics = dr["IsItalics"].ToString();
                    string pScaleCat_POSItemFontColor = dr["POSItemFontColor"].ToString();

                    ochk1.InsertScaleCategoryInWebOffice(Settings.CentralCustomerID, pCat_ID, pScaleCat_Name, pScaleCat_Ref_Department_ID,
                                    pScaleCat_POSBackground, pScaleCat_POSScreenStyle,
                                    pScaleCat_POSScreenColor, pScaleCat_POSFontType,
                                    pScaleCat_POSFontSize, pScaleCat_POSFontColor,
                                    pScaleCat_IsBold, pScaleCat_IsItalics, pScaleCat_POSItemFontColor);

                }

                foreach (DataRow dr in dScale_Addr.Rows)
                {
                    string pSCALE_IP = dr["SCALE_IP"].ToString();
                    string pSCALE_LOCATION = dr["SCALE_LOCATION"].ToString();
                    int pSCALE_TYPE_A = GeneralFunctions.fnInt32(dr["SCALE_TYPE"].ToString());
                    string pPORT = dr["PORT"].ToString();
                    string pFILE_LOCATION = dr["FILE_LOCATION"].ToString();
                    int pSCALE_ID_NO = GeneralFunctions.fnInt32(dr["SCALE_ID_NO"].ToString());
                    string pSCALE_NOTES = dr["SCALE_NOTES"].ToString();
                    int pMil_Sec = GeneralFunctions.fnInt32(dr["Mil_Sec"].ToString());
                    string pRef_Department_ID = dr["DEPARTMENT_ID"].ToString();


                    ochk1.InsertScaleAddressInWebOffice(Settings.CentralCustomerID, pSCALE_IP, pSCALE_LOCATION, pSCALE_TYPE_A, pPORT,
                                    pFILE_LOCATION, pSCALE_ID_NO, pSCALE_NOTES,
                                    pMil_Sec, pRef_Department_ID);

                }

                foreach (DataRow dr in dScale_LabelFormat.Rows)
                {

                    string pLabelFormatName = dr["FormatName"].ToString();
                    string pIsDefaultFormat = dr["IsDefaultFormat"].ToString();
                    string pFormatType = dr["FormatType"].ToString();
                    byte[] pLabelFormat_RawData = (byte[])dr["LabelFormat"];
                    ochk1.InsertScaleLabelFormatInWebOffice(Settings.CentralCustomerID, pLabelFormatName, pIsDefaultFormat, pFormatType, pLabelFormat_RawData);
                }

                foreach (DataRow dr in dScale_GraphicArt.Rows)
                {
                    int pGraphic_ArtNo = GeneralFunctions.fnInt32(dr["ArtNo"].ToString());
                    byte[] pGraphic_GraphicArt_RawData = (byte[])dr["GraphicArt"];
                    ochk1.InsertGraphicArtInWebOffice(Settings.CentralCustomerID, pGraphic_ArtNo, pGraphic_GraphicArt_RawData);
                }

                foreach (DataRow dr in dScale_SecondLabelType.Rows)
                {

                    int pLabel_ID = GeneralFunctions.fnInt32(dr["Label_ID"].ToString());
                    int pScale_Type = GeneralFunctions.fnInt32(dr["Scale_Type"].ToString());
                    int pLabelFormat = GeneralFunctions.fnInt32(dr["LabelFormat"].ToString());
                    string pDescription = dr["Description"].ToString();
                    string pSCALE_IP = dr["Scale_Ip"].ToString();
                    string pLabelFormatName = dr["LabelFormatName"].ToString();
                    string pFormatType = dr["LabelFormatType"].ToString();

                    ochk1.InsertSecondScaleTypeInWebOffice(Settings.CentralCustomerID, pLabel_ID, pDescription, pScale_Type, pLabelFormat, pSCALE_IP,
                                                            pLabelFormatName, pFormatType);
                }

                foreach (DataRow dr in dScale_LabelType.Rows)
                {

                    int pLabel_Id = GeneralFunctions.fnInt32(dr["Label_Id"].ToString());
                    int pScale_Type = GeneralFunctions.fnInt32(dr["Scale_Type"].ToString());
                    int pDp_ID = GeneralFunctions.fnInt32(dr["Dp_ID"].ToString());
                    string pDescription = dr["Description"].ToString();
                    string pSCALE_IP = dr["Scale_Ip"].ToString();

                    ochk1.InsertScaleTypeInWebOffice(Settings.CentralCustomerID, pLabel_Id, pDp_ID, pDescription, pScale_Type, pSCALE_IP);
                }

                foreach (DataRow dr in dScale_Graphic.Rows)
                {

                    int pGraphic_Graphic_ID = GeneralFunctions.fnInt32(dr["Graphic_ID"].ToString());
                    int pGraphic_Scale_Type = GeneralFunctions.fnInt32(dr["Scale_Type"].ToString());
                    int pGraphic_GraphicArtNo = GeneralFunctions.fnInt32(dr["GraphicArtNo"].ToString());
                    string pGraphic_Description = dr["Description"].ToString();
                    string pGraphic_SCALE_IP = dr["Scale_Ip"].ToString();

                    ochk1.InsertScaleGraphicsInWebOffice(Settings.CentralCustomerID, pGraphic_Graphic_ID, pGraphic_Description, pGraphic_Scale_Type, pGraphic_GraphicArtNo, pGraphic_SCALE_IP);
                }
                */

                DataTable dtblP = new DataTable();
                DataTable dtblVendor = new DataTable();
                DataTable dtblPMtx = new DataTable();
                DataTable dtblPMtxOp = new DataTable();
                DataTable dtblPMtxVal = new DataTable();

                DataTable dtblDept = new DataTable();
                DataTable dtblCat = new DataTable();
                DataTable dtblBrand = new DataTable();
                DataTable dtblTax = new DataTable();
                DataTable dtblRTax = new DataTable();



                DataTable dtblPScaleHeader = new DataTable();
                DataTable dtblPScaleMultiLabel = new DataTable();



                dtblP.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblP.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtblP.Columns.Add("SKU3", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Description", System.Type.GetType("System.String"));
                dtblP.Columns.Add("BinLocation", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtblP.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtblP.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtblP.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtblP.Columns.Add("PromptForPrice", System.Type.GetType("System.String"));
                dtblP.Columns.Add("AddtoPOSScreen", System.Type.GetType("System.String"));
                dtblP.Columns.Add("AddToPOSCategoryScreen", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtblP.Columns.Add("LastCost", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtblP.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtblP.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtblP.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtblP.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Points", System.Type.GetType("System.String"));
                dtblP.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtblP.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtblP.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtblP.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtblP.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ProductNotes", System.Type.GetType("System.String"));
                dtblP.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtblP.Columns.Add("AllowZeroStock", System.Type.GetType("System.String"));
                dtblP.Columns.Add("DisplayStockinPOS", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtblP.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtblP.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtblP.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtblP.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtblP.Columns.Add("TaggedInInvoice", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Season", System.Type.GetType("System.String"));
                dtblP.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtblP.Columns.Add("CaseQty", System.Type.GetType("System.String"));
                dtblP.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtblP.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtblP.Columns.Add("BreakPackRatio", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerMinute", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerHour", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerDay", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerWeek", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerMonth", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalDeposit", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalMinHour", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalMinAmount", System.Type.GetType("System.String"));
                dtblP.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RepairCharge", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPrompt", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RepairPromptForCharge", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RepairPromptForTag", System.Type.GetType("System.String"));

                dtblP.Columns.Add("BrandCode", System.Type.GetType("System.String"));
                dtblP.Columns.Add("DepartmentCode", System.Type.GetType("System.String"));
                dtblP.Columns.Add("CategoryCode", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax1Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax1Rate", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax2Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax2Rate", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax3Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tax3Rate", System.Type.GetType("System.String"));

                dtblP.Columns.Add("RentTax1Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentTax1Rate", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentTax2Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentTax2Rate", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentTax3Name", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentTax3Rate", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Tare", System.Type.GetType("System.String"));

                dtblP.Columns.Add("Tare2", System.Type.GetType("System.String"));
                dtblP.Columns.Add("NonDiscountable", System.Type.GetType("System.String"));
                dtblP.Columns.Add("AddToScaleScreen", System.Type.GetType("System.String"));
                dtblP.Columns.Add("RentalPerHalfDay", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleBackground", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleScreenStyle", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleScreenColor", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleFontType", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleFontSize", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleFontColor", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleIsBold", System.Type.GetType("System.String"));
                dtblP.Columns.Add("ScaleIsItalics", System.Type.GetType("System.String"));
                dtblP.Columns.Add("Notes2", System.Type.GetType("System.String"));
                dtblP.Columns.Add("SplitWeight", System.Type.GetType("System.String"));
                dtblP.Columns.Add("UOM", System.Type.GetType("System.String"));
                


                dtblPMtxOp.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblPMtxOp.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtblPMtxOp.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtblPMtxOp.Columns.Add("Option3Name", System.Type.GetType("System.String"));

                dtblPMtxVal.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblPMtxVal.Columns.Add("ValueID", System.Type.GetType("System.String"));
                dtblPMtxVal.Columns.Add("OptionValue", System.Type.GetType("System.String"));
                dtblPMtxVal.Columns.Add("OptionDefault", System.Type.GetType("System.String"));

                dtblPMtx.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblPMtx.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                dtblPMtx.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                dtblPMtx.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                dtblPMtx.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));

                dtblVendor.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("VendorCode", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("Price", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("IsPrimary", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("PackQty", System.Type.GetType("System.String"));
                dtblVendor.Columns.Add("Shrink", System.Type.GetType("System.String"));






                dtblPScaleHeader.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("row_id", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SCALE_DESCRIPTION_1", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SCALE_DESCRIPTION_2", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("ITEM_TYPE", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("BYCOUNT", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("WEIGHT", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SHELF_LIFE", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("PRODUCT_LIFE", System.Type.GetType("System.String"));


                dtblPScaleHeader.Columns.Add("INGRED_STATEMENT", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SPECIAL_MESSAGE", System.Type.GetType("System.String"));

                dtblPScaleHeader.Columns.Add("FORCE_TARE", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("TARE_1_S", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("TARE_2_O", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("TempNum", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("ServSize", System.Type.GetType("System.String"));

                dtblPScaleHeader.Columns.Add("ServPC", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Calors", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("FatCalors", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("TotFat", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("FatPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SatFat", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SatFatPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Chol", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("CholPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Sodium", System.Type.GetType("System.String"));

                dtblPScaleHeader.Columns.Add("SodPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Carbs", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("CarbPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Fiber", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("FiberPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Sugar", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Proteins", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("CalcPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("IronPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("VitDPerc", System.Type.GetType("System.String"));

                dtblPScaleHeader.Columns.Add("VitEPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("VitCPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("VitAPerc", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("TransFattyAcid", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("EXT1", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("EXT2", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("PL_JulianDate", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SL_JulianDate", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("ScaleDisplayOrder", System.Type.GetType("System.String"));

                dtblPScaleHeader.Columns.Add("SugarAlcoh", System.Type.GetType("System.String"));
                dtblPScaleHeader.Columns.Add("SugarAlcohPerc", System.Type.GetType("System.String"));




                dtblPScaleMultiLabel.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtblPScaleMultiLabel.Columns.Add("row_id", System.Type.GetType("System.String"));
                dtblPScaleMultiLabel.Columns.Add("Type", System.Type.GetType("System.String"));
                dtblPScaleMultiLabel.Columns.Add("Value", System.Type.GetType("System.String"));
                dtblPScaleMultiLabel.Columns.Add("LinkID", System.Type.GetType("System.String"));
                dtblPScaleMultiLabel.Columns.Add("IP", System.Type.GetType("System.String"));


                WriteToCloudFile("Prepare final product records to Export");

                if (dProduct.Rows.Count > 0)
                {
                    txtExportStatus.Text = "Prepare data to Export Product -start";
                    this.txtExportStatus.Refresh();
                    

                    foreach (DataRow drp in dProduct.Rows)
                    {
                        try
                        {
                            int pid = GeneralFunctions.fnInt32(drp["ID"].ToString());
                            int brid = GeneralFunctions.fnInt32(drp["BrandID"].ToString());
                            int dpid = GeneralFunctions.fnInt32(drp["DepartmentID"].ToString());
                            int ctid = GeneralFunctions.fnInt32(drp["CategoryID"].ToString());

                            string pty = drp["ProductType"].ToString();

                            string subbr1 = "";

                            string subdp1 = "";


                            string subct1 = "";


                            string subtx1 = "";
                            string subtx11 = "0";
                            string subtx2 = "";
                            string subtx22 = "0";
                            string subtx3 = "";
                            string subtx33 = "0";

                            string subrtx1 = "";
                            string subrtx11 = "0";
                            string subrtx2 = "";
                            string subrtx22 = "0";
                            string subrtx3 = "";
                            string subrtx33 = "0";


                            PosDataObject.Central obj2 = new PosDataObject.Central();
                            obj2.Connection = SystemVariables.Conn;
                            dtblDept = obj2.FetchProductDepartment(dpid);

                            foreach (DataRow deptdr in dtblDept.Rows)
                            {
                                subdp1 = deptdr["DepartmentID"].ToString();
                            }

                            PosDataObject.Central obj3 = new PosDataObject.Central();
                            obj3.Connection = SystemVariables.Conn;
                            dtblCat = obj3.FetchProductCategory(ctid);

                            foreach (DataRow ctdr in dtblCat.Rows)
                            {
                                subct1 = ctdr["CategoryID"].ToString();
                            }

                            if (brid > 0)
                            {
                                PosDataObject.Central obj4 = new PosDataObject.Central();
                                obj4.Connection = SystemVariables.Conn;
                                dtblBrand = obj4.FetchProductBrand(brid);
                                foreach (DataRow brdr in dtblBrand.Rows)
                                {
                                    subbr1 = brdr["BrandID"].ToString();
                                }
                            }

                            PosDataObject.Central obj5 = new PosDataObject.Central();
                            obj5.Connection = SystemVariables.Conn;
                            dtblTax = obj5.FetchProductTaxes(pid);

                            if (dtblTax.Rows.Count > 0)
                            {
                                int tcnt = 1;
                                foreach (DataRow txdr in dtblTax.Rows)
                                {
                                    if (tcnt == 1)
                                    {
                                        subtx1 = txdr["TaxName"].ToString();
                                        subtx11 = txdr["TaxRate"].ToString();
                                    }
                                    if (tcnt == 2)
                                    {
                                        subtx2 = txdr["TaxName"].ToString();
                                        subtx22 = txdr["TaxRate"].ToString();
                                    }
                                    if (tcnt == 3)
                                    {
                                        subtx3 = txdr["TaxName"].ToString();
                                        subtx33 = txdr["TaxRate"].ToString();
                                    }
                                    tcnt++;

                                }
                            }

                            PosDataObject.Central obj77 = new PosDataObject.Central();
                            obj77.Connection = SystemVariables.Conn;
                            dtblRTax = obj77.FetchProductRentTaxes(pid);
                            //Console.WriteLine("GetTaxes");
                            if (dtblRTax.Rows.Count > 0)
                            {
                                int tcnt = 1;
                                foreach (DataRow txdr in dtblRTax.Rows)
                                {
                                    if (tcnt == 1)
                                    {
                                        subrtx1 = txdr["TaxName"].ToString();
                                        subrtx11 = txdr["TaxRate"].ToString();
                                    }
                                    if (tcnt == 2)
                                    {
                                        subrtx2 = txdr["TaxName"].ToString();
                                        subrtx22 = txdr["TaxRate"].ToString();
                                    }
                                    if (tcnt == 3)
                                    {
                                        subrtx3 = txdr["TaxName"].ToString();
                                        subrtx33 = txdr["TaxRate"].ToString();
                                    }
                                    tcnt++;

                                }
                            }

                            DataTable dtblV = new DataTable();
                            PosDataObject.Central obj6 = new PosDataObject.Central();
                            obj6.Connection = SystemVariables.Conn;
                            dtblV = obj6.FetchProductPrimayVendor(pid);
                            //Console.WriteLine("GetPrimayVendor");
                            foreach (DataRow dv1 in dtblV.Rows)
                            {
                                dtblVendor.Rows.Add(new object[] {
                                            dv1["SKU"].ToString(),
                                            dv1["VendorCode"].ToString(),
                                            dv1["PartNumber"].ToString(),
                                            dv1["Price"].ToString(),
                                            dv1["IsPrimary"].ToString(),
                                            dv1["PackQty"].ToString(),
                                            dv1["Shrink"].ToString()
                                        });
                            }


                            if (pty == "M") // Matrix Product
                            {
                                DataTable dtblM1 = new DataTable();
                                PosDataObject.Central obj81 = new PosDataObject.Central();
                                obj81.Connection = SystemVariables.Conn;
                                dtblM1 = obj81.FetchProductMatrixOptions(pid);
                                foreach (DataRow dv1 in dtblM1.Rows)
                                {
                                    dtblPMtxOp.Rows.Add(new object[] {
                                            dv1["SKU"].ToString(),
                                            dv1["Option1Name"].ToString(),
                                            dv1["Option2Name"].ToString(),
                                            dv1["Option3Name"].ToString()
                                        });
                                }


                                DataTable dtblM2 = new DataTable();
                                PosDataObject.Central obj82 = new PosDataObject.Central();
                                obj82.Connection = SystemVariables.Conn;
                                dtblM2 = obj82.FetchProductMatrixValues(pid);
                                foreach (DataRow dv1 in dtblM2.Rows)
                                {
                                    dtblPMtxVal.Rows.Add(new object[] {
                                            dv1["SKU"].ToString(),
                                            dv1["ValueID"].ToString(),
                                            dv1["OptionValue"].ToString(),
                                            dv1["OptionDefault"].ToString()
                                        });
                                }

                                DataTable dtblM3 = new DataTable();
                                PosDataObject.Central obj83 = new PosDataObject.Central();
                                obj83.Connection = SystemVariables.Conn;
                                dtblM3 = obj82.FetchProductMatrix(pid);
                                foreach (DataRow dv1 in dtblM3.Rows)
                                {
                                    dtblPMtx.Rows.Add(new object[] {

                                            dv1["SKU"].ToString(),
                                            dv1["OptionValue1"].ToString(),
                                            dv1["OptionValue2"].ToString(),
                                            dv1["OptionValue3"].ToString(),
                                            dv1["QtyOnHand"].ToString()
                                        });
                                }
                            }

                            // Fetch Scale Details

                            DataTable dtblTS1 = new DataTable();
                            PosDataObject.Central objscl1 = new PosDataObject.Central();
                            objscl1.Connection = SystemVariables.Conn;
                            dtblTS1 = objscl1.GetScaleProduct(pid);
                            foreach (DataRow dsc1 in dtblTS1.Rows)
                            {
                                dtblPScaleHeader.Rows.Add(new object[] {
                                                    drp["SKU"].ToString(),
                                                    dsc1["row_id"].ToString(),
                                                    dsc1["PLU_NUMBER"].ToString(),
                                                    dsc1["SCALE_DESCRIPTION_1"].ToString(),
                                                    dsc1["SCALE_DESCRIPTION_2"].ToString(),
                                                    dsc1["ITEM_TYPE"].ToString(),
                                                    dsc1["BYCOUNT"].ToString(),
                                                    dsc1["WEIGHT"].ToString(),
                                                    dsc1["SHELF_LIFE"].ToString(),
                                                    dsc1["PRODUCT_LIFE"].ToString(),
                                                    dsc1["INGRED_STATEMENT"].ToString(),
                                                    dsc1["SPECIAL_MESSAGE"].ToString(),
                                                    dsc1["FORCE_TARE"].ToString(),
                                                    dsc1["TARE_1_S"].ToString(),
                                                    dsc1["TARE_2_O"].ToString(),
                                                    dsc1["TempNum"].ToString(),
                                                    dsc1["ServSize"].ToString(),
                                                    dsc1["ServPC"].ToString(),
                                                    dsc1["Calors"].ToString(),
                                                    dsc1["FatCalors"].ToString(),
                                                    dsc1["TotFat"].ToString(),
                                                    dsc1["FatPerc"].ToString(),
                                                    dsc1["SatFat"].ToString(),
                                                    dsc1["SatFatPerc"].ToString(),
                                                    dsc1["Chol"].ToString(),
                                                    dsc1["CholPerc"].ToString(),
                                                    dsc1["Sodium"].ToString(),
                                                    dsc1["SodPerc"].ToString(),
                                                    dsc1["Carbs"].ToString(),
                                                    dsc1["CarbPerc"].ToString(),
                                                    dsc1["Fiber"].ToString(),
                                                    dsc1["FiberPerc"].ToString(),
                                                    dsc1["Sugar"].ToString(),
                                                    dsc1["Proteins"].ToString(),
                                                    dsc1["CalcPerc"].ToString(),
                                                    dsc1["IronPerc"].ToString(),
                                                    dsc1["VitDPerc"].ToString(),
                                                    dsc1["VitEPerc"].ToString(),
                                                    dsc1["VitCPerc"].ToString(),
                                                    dsc1["VitAPerc"].ToString(),
                                                    dsc1["TransFattyAcid"].ToString(),
                                                    dsc1["Cat_ID"].ToString(),
                                                    dsc1["EXT1"].ToString(),
                                                    dsc1["EXT2"].ToString(),
                                                    dsc1["PL_JulianDate"].ToString(),
                                                    dsc1["SL_JulianDate"].ToString(),
                                                    dsc1["ScaleDisplayOrder"].ToString(),
                                                    dsc1["SugarAlcoh"].ToString(),
                                                    dsc1["SugarAlcohPerc"].ToString()});



                                int irow_id = GeneralFunctions.fnInt32(dsc1["row_id"].ToString());


                                DataTable dtblTS2 = new DataTable();
                                PosDataObject.Central objscl2 = new PosDataObject.Central();
                                objscl2.Connection = SystemVariables.Conn;
                                dtblTS2 = objscl2.GetScaleMappingRecord(irow_id);
                                foreach (DataRow dsc2 in dtblTS2.Rows)
                                {
                                    string ip = "";
                                    if (dsc2["Type"].ToString() == "Label")
                                    {
                                        ip = objscl2.GetScaleLabelLinkIp(GeneralFunctions.fnInt32(dsc2["ID"].ToString()));
                                    }

                                    dtblPScaleMultiLabel.Rows.Add(new object[] {
                                                                                        drp["SKU"].ToString(),
                                                                                        irow_id,
                                                                                        dsc2["Type"].ToString(),
                                                                                        dsc2["Value"].ToString(),
                                                                                        dsc2["ID"].ToString(),
                                                                                        ip});


                                }
                            }




                            dtblP.Rows.Add(new object[] {
                                                    drp["SKU"].ToString(),
                                                    drp["SKU2"].ToString(),
                                                    drp["SKU3"].ToString(),
                                                    drp["Description"].ToString(),
                                                    drp["BinLocation"].ToString(),
                                                    drp["ProductType"].ToString(),
                                                    drp["PriceA"].ToString(),
                                                    drp["PriceB"].ToString(),
                                                    drp["PriceC"].ToString(),
                                                    drp["PromptForPrice"].ToString(),
                                                    drp["AddtoPOSScreen"].ToString(),
                                                    drp["AddToPOSCategoryScreen"].ToString(),
                                                    drp["ScaleBarCode"].ToString(),
                                                    drp["LastCost"].ToString(),
                                                    drp["Cost"].ToString(),
                                                    drp["DiscountedCost"].ToString(),
                                                    drp["QtyOnHand"].ToString(),
                                                    drp["QtyOnLayaway"].ToString(),
                                                    drp["ReorderQty"].ToString(),
                                                    drp["NormalQty"].ToString(),
                                                    drp["Points"].ToString(),
                                                    drp["PrintBarCode"].ToString(),
                                                    drp["NoPriceOnLabel"].ToString(),
                                                    drp["QtyToPrint"].ToString(),
                                                    drp["LabelType"].ToString(),
                                                    drp["FoodStampEligible"].ToString(),
                                                    drp["ProductNotes"].ToString(),
                                                    drp["MinimumAge"].ToString(),
                                                    drp["AllowZeroStock"].ToString(),
                                                    drp["DisplayStockinPOS"].ToString(),
                                                    drp["POSBackground"].ToString(),
                                                    drp["POSScreenStyle"].ToString(),
                                                    drp["POSScreenColor"].ToString(),
                                                    drp["POSFontType"].ToString(),
                                                    drp["POSFontSize"].ToString(),
                                                    drp["POSFontColor"].ToString(),
                                                    drp["IsBold"].ToString(),
                                                    drp["IsItalics"].ToString(),
                                                    drp["DecimalPlace"].ToString(),
                                                    drp["ProductStatus"].ToString(),
                                                    drp["TaggedInInvoice"].ToString(),
                                                    drp["Season"].ToString(),
                                                    drp["CaseUPC"].ToString(),
                                                    drp["CaseQty"].ToString(),
                                                    drp["UPC"].ToString(),
                                                    drp["LinkSKU"].ToString(),
                                                    drp["BreakPackRatio"].ToString(),
                                                    drp["RentalPerMinute"].ToString(),
                                                    drp["RentalPerHour"].ToString(),
                                                    drp["RentalPerDay"].ToString(),
                                                    drp["RentalPerWeek"].ToString(),
                                                    drp["RentalPerMonth"].ToString(),
                                                    drp["RentalDeposit"].ToString(),
                                                    drp["RentalMinHour"].ToString(),
                                                    drp["RentalMinAmount"].ToString(),
                                                    drp["MinimumServiceTime"].ToString(),
                                                    drp["RepairCharge"].ToString(),
                                                    drp["RentalPrompt"].ToString(),
                                                    drp["RepairPromptForCharge"].ToString(),
                                                    drp["RepairPromptForTag"].ToString(),
                                                    subbr1,
                                                    subdp1,
                                                    subct1,
                                                    subtx1,subtx11,subtx2,subtx22,subtx3,subtx33,
                                                    subrtx1,subrtx11,subrtx2,subrtx22,subrtx3,subrtx33,
                                                    drp["Tare"].ToString(),
                                                    drp["Tare2"].ToString(),
                                                    drp["NonDiscountable"].ToString(),
                                                    drp["AddToScaleScreen"].ToString(),
                                                    drp["RentalPerHalfDay"].ToString(),
                                                    drp["ScaleBackground"].ToString(),
                                                    drp["ScaleScreenStyle"].ToString(),
                                                    drp["ScaleScreenColor"].ToString(),
                                                    drp["ScaleFontType"].ToString(),
                                                    drp["ScaleFontSize"].ToString(),
                                                    drp["ScaleFontColor"].ToString(),
                                                    drp["ScaleIsBold"].ToString(),
                                                    drp["ScaleIsItalics"].ToString(),
                                                    drp["Notes2"].ToString(),
                                                    drp["SplitWeight"].ToString(),
                                                    drp["UOM"].ToString()
                                        });
                        }
                        catch (Exception ex)
                        {
                            WriteToCloudFile(ex.Message);
                            txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                            txtExportStatus.Refresh();
                            System.Threading.Thread.Sleep(1000);

                        }
                    }


                    txtExportStatus.Text = "Prepare data to Export Product -end";
                    this.txtExportStatus.Refresh();
                }

                int pi = 0;
                expcnt = 0;

                if (dtblP.Rows.Count > 0)
                {
                    WriteToCloudFile("Exporting product: " + dtblP.Rows.Count.ToString());
                }

                foreach (DataRow dr in dtblP.Rows)
                {
                    expcnt++;

                    txtExportStatus.Text = "Export Product: " + expcnt.ToString() + "/" + dtblP.Rows.Count.ToString();
                    this.txtExportStatus.Refresh();


                    pi++;
                    int pID = 0;
                    int MOID = 0;
                    string prty = dr["ProductType"].ToString();
                    try
                    {
                        PosDataObject.Central dobp = new PosDataObject.Central();
                        dobp.Connection = SystemVariables.rmConn;

                        dobp.pSKU = dr["SKU"].ToString();
                        dobp.pSKU2 = dr["SKU2"].ToString();
                        dobp.pSKU3 = dr["SKU3"].ToString();
                        dobp.pDescription = dr["Description"].ToString();
                        dobp.pBinLocation = dr["BinLocation"].ToString();
                        dobp.pProductNotes = dr["ProductNotes"].ToString();
                        dobp.pPOSScreenColor = dr["POSScreenColor"].ToString();
                        dobp.pPOSScreenStyle = dr["POSScreenStyle"].ToString();
                        dobp.pPOSBackground = dr["POSBackground"].ToString();
                        dobp.pPOSFontType = dr["POSFontType"].ToString();
                        dobp.pPOSFontColor = dr["POSFontColor"].ToString();
                        dobp.pIsBold = dr["IsBold"].ToString();
                        dobp.pIsItalics = dr["IsItalics"].ToString();
                        dobp.pCaseUPC = dr["CaseUPC"].ToString();
                        dobp.pUPC = dr["UPC"].ToString();
                        dobp.pSeason = dr["Season"].ToString();
                        dobp.pProductType = dr["ProductType"].ToString();
                        dobp.pPromptForPrice = dr["PromptForPrice"].ToString();
                        dobp.pPrintBarCode = dr["PrintBarCode"].ToString();
                        dobp.pNoPriceOnLabel = dr["NoPriceOnLabel"].ToString();
                        dobp.pFoodStampEligible = dr["FoodStampEligible"].ToString();
                        dobp.pAddtoPOSScreen = dr["AddtoPOSScreen"].ToString();
                        dobp.pAddToPOSCategoryScreen = dr["AddToPOSCategoryScreen"].ToString();
                        dobp.pScaleBarCode = dr["ScaleBarCode"].ToString();
                        dobp.pAllowZeroStock = dr["AllowZeroStock"].ToString();
                        dobp.pDisplayStockinPOS = dr["DisplayStockinPOS"].ToString();
                        dobp.pProductStatus = dr["ProductStatus"].ToString();
                        dobp.pRentalPrompt = dr["RentalPrompt"].ToString();
                        dobp.pRepairPromptForCharge = dr["RepairPromptForCharge"].ToString();
                        dobp.pRepairPromptForTag = dr["RepairPromptForTag"].ToString();
                        dobp.pDepartmentCode = dr["DepartmentCode"].ToString();
                        dobp.pCategoryCode = dr["CategoryCode"].ToString();
                        dobp.pTax1Name = dr["Tax1Name"].ToString();
                        dobp.pTax2Name = dr["Tax2Name"].ToString();
                        dobp.pTax3Name = dr["Tax3Name"].ToString();
                        dobp.pBrandCode = dr["BrandCode"].ToString();
                        dobp.pPOSFontSize = GeneralFunctions.fnInt32(dr["POSFontSize"].ToString());
                        dobp.pQtyToPrint = GeneralFunctions.fnInt32(dr["QtyToPrint"].ToString());
                        dobp.pLabelType = GeneralFunctions.fnInt32(dr["LabelType"].ToString());
                        dobp.pPoints = GeneralFunctions.fnInt32(dr["Points"].ToString());
                        dobp.pMinimumAge = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString());
                        dobp.pDecimalPlace = GeneralFunctions.fnInt32(dr["DecimalPlace"].ToString());
                        dobp.pCaseQty = GeneralFunctions.fnInt32(dr["CaseQty"].ToString());
                        dobp.pLinkSKU = GeneralFunctions.fnInt32(dr["LinkSKU"].ToString());
                        dobp.pMinimumServiceTime = GeneralFunctions.fnInt32(dr["MinimumServiceTime"].ToString());

                        dobp.pPriceA = GeneralFunctions.fnDouble(dr["PriceA"].ToString());
                        dobp.pPriceB = GeneralFunctions.fnDouble(dr["PriceB"].ToString());
                        dobp.pPriceC = GeneralFunctions.fnDouble(dr["PriceC"].ToString());
                        dobp.pLastCost = GeneralFunctions.fnDouble(dr["LastCost"].ToString());
                        dobp.pCost = GeneralFunctions.fnDouble(dr["Cost"].ToString());
                        dobp.pDCost = GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());
                        dobp.pQtyOnHand = GeneralFunctions.fnDouble(dr["QtyOnHand"].ToString());
                        dobp.pQtyOnLayaway = GeneralFunctions.fnDouble(dr["QtyOnLayaway"].ToString());
                        dobp.pReorderQty = GeneralFunctions.fnDouble(dr["ReorderQty"].ToString());
                        dobp.pNormalQty = GeneralFunctions.fnDouble(dr["NormalQty"].ToString());
                        dobp.pBreakPackRatio = GeneralFunctions.fnDouble(dr["BreakPackRatio"].ToString());
                        dobp.pRentalPerMinute = GeneralFunctions.fnDouble(dr["RentalPerMinute"].ToString());
                        dobp.pRentalPerHour = GeneralFunctions.fnDouble(dr["RentalPerHour"].ToString());
                        dobp.pRentalPerDay = GeneralFunctions.fnDouble(dr["RentalPerDay"].ToString());
                        dobp.pRentalPerWeek = GeneralFunctions.fnDouble(dr["RentalPerWeek"].ToString());
                        dobp.pRentalPerMonth = GeneralFunctions.fnDouble(dr["RentalPerMonth"].ToString());
                        dobp.pRentalDeposit = GeneralFunctions.fnDouble(dr["RentalDeposit"].ToString());
                        dobp.pRentalMinHour = GeneralFunctions.fnDouble(dr["RentalMinHour"].ToString());
                        dobp.pRentalMinAmount = GeneralFunctions.fnDouble(dr["RentalMinAmount"].ToString());
                        dobp.pRepairCharge = GeneralFunctions.fnDouble(dr["RepairCharge"].ToString());
                        dobp.pTax1Rate = GeneralFunctions.fnDouble(dr["Tax1Rate"].ToString());
                        dobp.pTax2Rate = GeneralFunctions.fnDouble(dr["Tax2Rate"].ToString());
                        dobp.pTax3Rate = GeneralFunctions.fnDouble(dr["Tax3Rate"].ToString());
                        dobp.pRntTax1Name = dr["RentTax1Name"].ToString();
                        dobp.pRntTax2Name = dr["RentTax2Name"].ToString();
                        dobp.pRntTax3Name = dr["RentTax3Name"].ToString();
                        dobp.pRntTax1Rate = GeneralFunctions.fnDouble(dr["RentTax1Rate"].ToString());
                        dobp.pRntTax2Rate = GeneralFunctions.fnDouble(dr["RentTax2Rate"].ToString());
                        dobp.pRntTax3Rate = GeneralFunctions.fnDouble(dr["RentTax3Rate"].ToString());
                        dobp.pTare = GeneralFunctions.fnDouble(dr["Tare"].ToString());

                        dobp.pScaleScreenColor = dr["ScaleScreenColor"].ToString();
                        dobp.pScaleScreenStyle = dr["ScaleScreenStyle"].ToString();
                        dobp.pScaleBackground = dr["ScaleBackground"].ToString();
                        dobp.pScaleFontType = dr["ScaleFontType"].ToString();
                        dobp.pScaleFontColor = dr["ScaleFontColor"].ToString();
                        dobp.pScaleIsBold = dr["ScaleIsBold"].ToString();
                        dobp.pScaleIsItalics = dr["ScaleIsItalics"].ToString();
                        dobp.pTare2 = GeneralFunctions.fnDouble(dr["Tare2"].ToString());
                        dobp.pAddtoScaleScreen = dr["AddtoScaleScreen"].ToString();
                        dobp.pNonDiscountable = dr["NonDiscountable"].ToString();
                        dobp.pRentalPerHalfDay = GeneralFunctions.fnDouble(dr["RentalPerHalfDay"].ToString());
                        dobp.pScaleFontSize = GeneralFunctions.fnInt32(dr["ScaleFontSize"].ToString());
                        dobp.pPNotes2 = dr["Notes2"].ToString();
                        dobp.SplitWeight = GeneralFunctions.fnDouble(dr["SplitWeight"].ToString());
                        dobp.UOM = dr["UOM"].ToString();
                        string err = dobp.InsertProductInWebOffice(Settings.CentralCustomerID, txtStoreCode.Text.Trim());

                        pID = dobp.pPID;

                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                        this.txtExportStatus.Refresh();
                    }

                    if (pID > 0)
                    {

                        /*
                        DataTable dtblScaleDetails_Specific = dtblPScaleHeader.Clone();
                        DataTable dtblScaleLabelMapping = dtblPScaleMultiLabel.Clone();

                        DataRow[] drTmp1 = dtblPScaleHeader.Select("SKU = '" + dr["SKU"].ToString() + "'");

                        foreach (DataRow r1 in drTmp1)
                        {
                            dtblScaleDetails_Specific.ImportRow(r1);
                        }


                        if (dtblScaleDetails_Specific.Rows.Count > 0)
                        {
                            DataRow[] drTmp2 = dtblPScaleMultiLabel.Select("SKU = '" + dr["SKU"].ToString() + "'");

                            foreach (DataRow r2 in drTmp2)
                            {
                                dtblScaleLabelMapping.ImportRow(r2);
                            }
                        }

                        foreach (DataRow driSd in dtblScaleDetails_Specific.Rows)
                        {
                            int ref_ScaleRowID = 0;
                            string S_dtl_err = "";
                            PosDataObject.Central objsc_1 = new PosDataObject.Central();
                            objsc_1.Connection = SystemVariables.rmConn;

                            S_dtl_err = objsc_1.InsertScaleDetailsInWebOffice(Settings.CentralCustomerID,
                                                pID,
                                                driSd["PLU_NUMBER"].ToString(),
                                                driSd["SCALE_DESCRIPTION_1"].ToString(),
                                                driSd["SCALE_DESCRIPTION_2"].ToString(),
                                                driSd["ITEM_TYPE"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["BYCOUNT"].ToString()),
                                                GeneralFunctions.fnDouble(driSd["WEIGHT"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["SHELF_LIFE"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["PRODUCT_LIFE"].ToString()),
                                                driSd["INGRED_STATEMENT"].ToString(),
                                                driSd["SPECIAL_MESSAGE"].ToString(),
                                                driSd["FORCE_TARE"].ToString(),
                                                GeneralFunctions.fnDouble(driSd["TARE_1_S"].ToString()),
                                                GeneralFunctions.fnDouble(driSd["TARE_2_O"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["TempNum"].ToString()),
                                                driSd["ServSize"].ToString(),
                                                driSd["ServPC"].ToString(),
                                                driSd["Calors"].ToString(),
                                                driSd["FatCalors"].ToString(),
                                                driSd["TotFat"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["FatPerc"].ToString()),
                                                driSd["SatFat"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["SatFatPerc"].ToString()),
                                                driSd["Chol"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["CholPerc"].ToString()),
                                                driSd["Sodium"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["SodPerc"].ToString()),
                                                driSd["Carbs"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["CarbPerc"].ToString()),
                                                driSd["Fiber"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["FiberPerc"].ToString()),
                                                driSd["Sugar"].ToString(),
                                                driSd["Proteins"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["CalcPerc"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["IronPerc"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["VitDPerc"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["VitEPerc"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["VitCPerc"].ToString()),
                                                GeneralFunctions.fnInt32(driSd["VitAPerc"].ToString()),
                                                driSd["TransFattyAcid"].ToString(),
                                                driSd["Cat_ID"].ToString(),
                                                driSd["EXT1"].ToString(),
                                                driSd["EXT2"].ToString(),
                                                driSd["PL_JulianDate"].ToString(),
                                                driSd["SL_JulianDate"].ToString(),
                                                driSd["SugarAlcoh"].ToString(),
                                                GeneralFunctions.fnInt32(driSd["SugarAlcohPerc"].ToString()),
                                                ref ref_ScaleRowID);


                            if (dtblScaleLabelMapping.Rows.Count > 0)
                            {
                                foreach (DataRow driSlb in dtblScaleLabelMapping.Rows)
                                {
                                    PosDataObject.Central dobp44 = new PosDataObject.Central();
                                    dobp44.Connection = SystemVariables.rmConn;
                                    dobp44.InsertScaleMappingInWebOffice(Settings.CentralCustomerID, ref_ScaleRowID, driSlb["Type"].ToString(), GeneralFunctions.fnInt32(driSlb["Value"].ToString()), driSlb["IP"].ToString());

                                }
                            }
                        }

                        */




                        foreach (DataRow dr1 in dtblVendor.Rows)
                        {
                            if (dr["SKU"].ToString() != dr1["SKU"].ToString()) continue;

                            try
                            {
                                PosDataObject.Central dobp1 = new PosDataObject.Central();
                                dobp1.Connection = SystemVariables.rmConn;
                                string err = dobp1.InsertPrimaryVendorInWebOffice(Settings.CentralCustomerID, pID, dr1["VendorCode"].ToString(),
                                                                        dr1["PartNumber"].ToString(), GeneralFunctions.fnDouble(dr1["Price"].ToString()),
                                                                        dr1["IsPrimary"].ToString(), GeneralFunctions.fnInt32(dr1["PackQty"].ToString()),
                                                                        GeneralFunctions.fnDouble(dr1["Shrink"].ToString()));
                            }
                            catch (Exception ex)
                            {
                                txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                                this.txtExportStatus.Refresh();
                            }
                        }


                        if (prty == "M") // Matrix Item
                        {
                            foreach (DataRow drm1 in dtblPMtxOp.Rows)
                            {
                                MOID = 0;
                                if (dr["SKU"].ToString() != drm1["SKU"].ToString()) continue;
                                try
                                {
                                    PosDataObject.Central dobp34 = new PosDataObject.Central();
                                    dobp34.Connection = SystemVariables.rmConn;
                                    string err = dobp34.InsertProductMatrixOptionsInWebOffice(Settings.CentralCustomerID, pID, drm1["Option1Name"].ToString(), drm1["Option2Name"].ToString(),
                                        drm1["Option3Name"].ToString(), ref MOID);
                                }
                                catch (Exception ex)
                                {
                                    txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                                    this.txtExportStatus.Refresh();
                                }

                                foreach (DataRow drm2 in dtblPMtxVal.Rows)
                                {
                                    if (dr["SKU"].ToString() != drm2["SKU"].ToString()) continue;
                                    try
                                    {
                                        PosDataObject.Central dobp35 = new PosDataObject.Central();
                                        dobp35.Connection = SystemVariables.rmConn;
                                        string err = dobp35.InsertProductMatrixValuesInWebOffice(Settings.CentralCustomerID, MOID, GeneralFunctions.fnInt32(drm2["ValueID"].ToString()), drm2["OptionValue"].ToString(),
                                            drm2["OptionDefault"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                                        txtExportStatus.Refresh();
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }

                                foreach (DataRow drm3 in dtblPMtx.Rows)
                                {
                                    if (dr["SKU"].ToString() != drm3["SKU"].ToString()) continue;
                                    try
                                    {
                                        PosDataObject.Central dobp36 = new PosDataObject.Central();
                                        dobp36.Connection = SystemVariables.rmConn;
                                        string err = dobp36.InsertProductMatrixInWebOffice(Settings.CentralCustomerID, MOID, drm3["OptionValue1"].ToString(), drm3["OptionValue2"].ToString(),
                                            drm3["OptionValue3"].ToString(), GeneralFunctions.fnDouble(drm3["QtyOnHand"].ToString()));
                                    }
                                    catch (Exception ex)
                                    {
                                        txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                                        this.txtExportStatus.Refresh();
                                    }
                                }
                            }
                        }
                    }
                }

                try
                {
                    AddActionInGwareHost(txtStoreCode.Text.Trim(), "Export Existing Master Data to XEPOS Host - End");

                    txtExportStatus.Text = "Export Existing Master Data to XEPOS Host - End";
                    txtExportStatus.Refresh();
                    System.Threading.Thread.Sleep(1000);
                }
                catch(Exception ex)
                {
                    txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                    this.txtExportStatus.Refresh();

                }

            }
            catch(Exception ex)
            {
                WriteToCloudFile(ex.Message);
                txtExportStatus.Text = txtExportStatus.Text + "\r\n" + ex.Message;
                this.txtExportStatus.Refresh();
                
            }
            finally
            {
                WriteToCloudFile("Action: Export existing Master Data to XEPOS Host" + " : end");
                Cursor = Cursors.Arrow;
                DocMessage.MsgInformation(Properties.Resources.Data_Exported_successfully_to_XEPOS_Host);
            }
        }

        private void ChkgcRepeat_Checked(object sender, RoutedEventArgs e)
        {
            if (chkgcRepeat.IsChecked == true)
            {
                rep1.Visibility = Visibility.Visible;
                rep2.Visibility = Visibility.Visible;
                rep3.Visibility = Visibility.Visible;
                rep4.Visibility = Visibility.Visible;
                rep5.Visibility = Visibility.Visible;
            }
            else
            {
                rep1.Visibility = Visibility.Hidden;
                rep2.Visibility = Visibility.Hidden;
                rep3.Visibility = Visibility.Hidden;
                rep4.Visibility = Visibility.Hidden;
                rep5.Visibility = Visibility.Hidden;
            }
        }

        private void ChkIgcRepeat_Checked(object sender, RoutedEventArgs e)
        {
            if (chkIgcRepeat.IsChecked == true)
            {
                repi1.Visibility = Visibility.Visible;
                repi2.Visibility = Visibility.Visible;
                repi3.Visibility = Visibility.Visible;
                repi4.Visibility = Visibility.Visible;
                repi5.Visibility = Visibility.Visible;
            }
            else
            {
                repi1.Visibility = Visibility.Hidden;
                repi2.Visibility = Visibility.Hidden;
                repi3.Visibility = Visibility.Hidden;
                repi4.Visibility = Visibility.Hidden;
                repi5.Visibility = Visibility.Hidden;
            }
        }

        private void TxtStoreCode_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void RunScheduler(string ScheduleName)
        {
            string varaction = "";
            if (ScheduleName.Contains("Web Office General Export")) varaction = "Action: " + "Run scheduler manually to export Sales, inventory to Host";
            if (ScheduleName.Contains("Web Office GC Export")) varaction = "Action: " + "Run scheduler manually to export Customer, Gift Cert updates to Host";
            if (ScheduleName.Contains("Web Office General Import")) varaction = "Action: " + "Run scheduler manually to import Sales, inventory from Host";
            if (ScheduleName.Contains("Web Office GC Import")) varaction = "Action: " + "Run scheduler manually to import Customer, Gift Cert updates from Host";
            WriteToCloudFile("");
            WriteToCloudFile(varaction + " - start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);
            if (Settings.CentralExportImport == "Y")
            {

                if (!CheckAndSetParameterBeforeTaskSchedulerActivity())
                {
                    WriteToCloudFile("Host connection fails");
                    WriteToCloudFile(varaction + " - end");
                    return;
                }
                

                Cursor = Cursors.Wait;
                System.Threading.Thread.Sleep(100);
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + ScheduleName) != null;
                    if (boolFindTask)
                    {
                        WriteToCloudFile("Try to run Task Scheduler");
                        ts.GetTask(@"" + ScheduleName).Run();
                    }
                }
                catch(Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    WriteToCloudFile(varaction + " - end");
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }


            }
        }

        private void SetScheduleEndTime(DateEdit Stime, DateEdit Etime)
        {
            bool nextday = false;
            TimeSpan ets = new TimeSpan(Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            TimeSpan sts = new TimeSpan(Stime.DateTime.Hour, Stime.DateTime.Minute, Stime.DateTime.Second);
            TimeSpan rts = ets.Subtract(sts);

            if ((Stime.DateTime.Hour < 12) && (Etime.DateTime.Hour < 12))
            {
                if (rts.TotalMinutes < 0) nextday = true; else nextday = false;
            }

            if ((Stime.DateTime.Hour < 12) && (Etime.DateTime.Hour >= 12))
            {
                nextday = false;
            }

            if ((Stime.DateTime.Hour >= 12) && (Etime.DateTime.Hour < 12))
            {
                nextday = true;
            }

            if ((Stime.DateTime.Hour >= 12) && (Etime.DateTime.Hour >= 12))
            {
                if (rts.TotalMinutes < 0) nextday = true; else nextday = false;
            }
            if (nextday)
            {
                DateTime dt = Stime.DateTime.AddDays(1);
                Etime.DateTime = new DateTime(dt.Year, dt.Month, dt.Day, Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            }
            else
            {
                DateTime dt = Stime.DateTime;
                Etime.DateTime = new DateTime(dt.Year, dt.Month, dt.Day, Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            }
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            string actionprefx = (sender as Button).Content.ToString();
            WriteToCloudFile("");
            WriteToCloudFile("Action: " + actionprefx + " For Export Sales, Inventory to Host" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);
            if (!CheckAndSetParameterBeforeTaskSchedulerActivity())
            {
                WriteToLogFile("Before Setup Scheduler : Host Connection fails");
                WriteToCloudFile("Host Connection fails");
                WriteToCloudFile("Action: " + actionprefx + " For Export Sales, Inventory to Host" + " : end");
                return;
            }

            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "StoreToCOExport.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\StoreToCOExport.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                WriteToCloudFile("Try to set scheduler in Win 10");
                WriteToLogFile("Win 10 : start set scheduler");

                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName));

                WriteToLogFile("Win 10 : Delete scheduler");

                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName));
                    WriteToLogFile("Win 10 : Create scheduler");
                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  E|A";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    WriteToLogFile("Win 10 : Set Account Information");
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtTime.DateTime.Hour, (short)txtTime.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    WriteToLogFile("Win 10 : Set Trigger");
                    t.Save();
                    t.Close();
                    WriteToLogFile("Scheduler successfully set");
                    WriteToCloudFile("Scheduler successfully set");
                    if (btnSchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnSchedule.Content = "Modify Task Scheduler".ToUpper();
                        btnSchedule.Tag = "Modify Task Scheduler";
                        btnRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }
                }
                catch (Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    WriteToLogFile("win 10 scheduler setup error : " + ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToLogFile("Win 10 : end set scheduler");
                WriteToCloudFile("Action: " + actionprefx + " For Export Sales, Inventory to Host" + " : end");
            }

            if (Settings.OSVersion == "Win 11")
            {
                WriteToCloudFile("Try to set scheduler in Win 11");
                try
                {

                    WriteToLogFile("Win 11 : start set scheduler");

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);

                        WriteToLogFile("Win 11 : Delete scheduler");
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;
                    WriteToLogFile("Win 11 : Create scheduler");

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();
                    WriteToLogFile("Win 11 : Set Trigger Start");
                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtTime.DateTime.Minute);
                    daily.DaysInterval = 1;

                    td.Triggers.Add(daily);
                    WriteToLogFile("Win 11 : Set Trigger End");
                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  E|A", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);
                    WriteToLogFile("Win 11 : Register Task");
                    if (btnSchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                       
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnSchedule.Content = "Modify Task Scheduler".ToUpper();
                        btnSchedule.Tag = "Modify Task Scheduler";
                        btnRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }

                    WriteToLogFile("Scheduler successfully set");
                    WriteToCloudFile("Scheduler successfully set");
                }
                catch(Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    WriteToLogFile("win 11 scheduler setup error : " + ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Export Sales, Inventory to Host" + " : end");
            }

        }

        private void BtnRunSchedule_Click(object sender, RoutedEventArgs e)
        {
            
            RunScheduler(SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName));
        }

        private void BtnGCSchedule_Click(object sender, RoutedEventArgs e)
        {
            string actionprefx = (sender as Button).Content.ToString();
            WriteToCloudFile("");
            WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);

            if (!CheckAndSetParameterBeforeTaskSchedulerActivity())
            {
                WriteToCloudFile("Host Connection fails");
                WriteToLogFile("Before Setting Scheduler : Host Connection fails");
                WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : end");
                return;
            }


            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "StoreToCOExport.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\StoreToCOExport.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                WriteToCloudFile("Try to set scheduler in Win 10");
                WriteToLogFile("Win 10 : start set scheduler");
                try
                {
                    ScheduledTasks st = new ScheduledTasks();
                    st.DeleteTask(SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName));
                    WriteToLogFile("Win 10 : Delete scheduler");
                    try
                    {
                        ScheduledTasks st1 = new ScheduledTasks();

                        TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName));
                        WriteToLogFile("Win 10 : Create scheduler");
                        t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                        t.ApplicationName = csConnPath;
                        t.Parameters = "  E|G";
                        t.SetAccountInformation(Environment.UserName, (String)null);
                        WriteToLogFile("Win 10 : Set Account Information");
                        TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtgcTime.DateTime.Hour, (short)txtgcTime.DateTime.Minute, 1);
                        WriteToLogFile("Win 10 : Set Trigger Start");
                        t.Triggers.Add(dt);
                        if (chkgcRepeat.IsChecked == true)
                        {
                            WriteToLogFile("Win 10 : Set Repeat Trigger Start");
                            SetScheduleEndTime(txtgcTime, rep3);
                            int days = 0;
                            if (rep3.DateTime.Date > txtgcTime.DateTime.Date) days = 1;

                            TimeSpan ets = new TimeSpan(days, rep3.DateTime.Hour, rep3.DateTime.Minute, rep3.DateTime.Second);
                            TimeSpan sts = new TimeSpan(txtgcTime.DateTime.Hour, txtgcTime.DateTime.Minute, txtgcTime.DateTime.Second);
                            TimeSpan rts = ets.Subtract(sts);


                            dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                            if (rep2.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep1.Value.ToString());
                            else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep1.Value.ToString()) * 60;

                            WriteToLogFile("Win 10 : Set Repeat Trigger End");
                        }

                        t.Save();
                        t.Close();

                        WriteToLogFile("Scheduler successfully set");
                        WriteToCloudFile("Scheduler successfully set");
                        
                        if (btnGCSchedule.Tag.ToString() == "Add Task Scheduler")
                        {
                            DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                            btnGCSchedule.Content = "Modify Task Scheduler".ToUpper();
                            btnGCSchedule.Tag = "Modify Task Scheduler";
                            btnGCRunSchedule.Visibility = Visibility.Visible;
                        }
                        else
                        {


                            DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteToCloudFile(ex.Message);
                        WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : end");
                        WriteToLogFile("win 10 scheduler setup error : " + ex.Message);
                        DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                    }
                }
                catch (Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : end");
                    WriteToLogFile("win 10 scheduler setup error : " + ex.Message);
                    
                }

                WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : end");
            }

            if (Settings.OSVersion == "Win 11")
            {
                WriteToCloudFile("Try to set scheduler in Win 11");
                WriteToLogFile("Win 11 : start set scheduler");
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                        WriteToLogFile("Win 11 : Delete scheduler");
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;
                    WriteToLogFile("Win 11 : Create scheduler");
                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();
                    WriteToLogFile("Win 11 : Set Trigger Start");
                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtgcTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtgcTime.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkgcRepeat.IsChecked == true)
                    {
                        WriteToLogFile("Win 11 : Set Repeat Trigger Start");
                        SetScheduleEndTime(txtgcTime, rep3);
                        int days = 0;
                        if (rep3.DateTime.Date > txtgcTime.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep3.DateTime.Hour, rep3.DateTime.Minute, rep3.DateTime.Second);
                        TimeSpan sts = new TimeSpan(txtgcTime.DateTime.Hour, txtgcTime.DateTime.Minute, txtgcTime.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep2.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep1.Value.ToString()) : GeneralFunctions.fnInt32(rep1.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;

                        WriteToLogFile("Win 11 : Set Repeat Trigger End");
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  E|G", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);
                    WriteToLogFile("Win 11 : Register Task");
                    if (btnGCSchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnGCSchedule.Content = "Modify Task Scheduler".ToUpper();
                        btnGCSchedule.Tag = "Modify Task Scheduler";
                        btnGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }

                    WriteToLogFile("Scheduler successfully set");
                    WriteToCloudFile("Scheduler successfully set");
                }
                catch(Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    WriteToLogFile("win 11 scheduler setup error : " + ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Export Customer, Gift Cert info. to Host" + " : end");
            }
        }

        private void BtnGCRunSchedule_Click(object sender, RoutedEventArgs e)
        {
            RunScheduler(SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName));
        }

        private void BtnISchedule_Click(object sender, RoutedEventArgs e)
        {
            string actionprefx = (sender as Button).Content.ToString();
            WriteToCloudFile("");
            WriteToCloudFile("Action: " + actionprefx + " For Import Sales, Inventory from Host" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);
            if (!CheckAndSetParameterBeforeTaskSchedulerActivity())
            {
                WriteToCloudFile("Host connection fails");
                WriteToCloudFile("Action: " + actionprefx + " For Import Sales, Inventory from Host" + " : end");
                return;
            }

            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "StoreToCOExport.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\StoreToCOExport.exe";
            }


            if (Settings.OSVersion == "Win 10")
            {
                WriteToCloudFile("Try to set scheduler in Win 10");
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  I|A";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtITime.DateTime.Hour, (short)txtITime.DateTime.Minute, 1);
                    t.Triggers.Add(dt);

                    t.Save();
                    t.Close();

                    WriteToCloudFile("Scheduler successfully set");

                    if (btnISchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnISchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnISchedule.Tag = "Modify Task Scheduler";
                        btnIRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }
                }
                catch (Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                   DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Import Sales, Inventory from Host" + " : end");
            }

            if (Settings.OSVersion == "Win 11")
            {
                WriteToCloudFile("Try to set scheduler in Win 11");
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtITime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtITime.DateTime.Minute);
                    daily.DaysInterval = 1;

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  I|A", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    WriteToCloudFile("Scheduler successfully set");

                    if (btnISchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnISchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnISchedule.Tag = "Modify Task Scheduler";
                        btnIRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }
                }
                catch(Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Import Sales, Inventory from Host" + " : end");
            }

        }

        private void BtnIRunSchedule_Click(object sender, RoutedEventArgs e)
        {
            RunScheduler(SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName));
        }

        private void BtnIGCSchedule_Click(object sender, RoutedEventArgs e)
        {
            string actionprefx = (sender as Button).Content.ToString();
            WriteToCloudFile("");
            WriteToCloudFile("Action: " + actionprefx + " For Import Customer, Gift Cert. updates from Host" + " : start");
            WriteToCloudFile("Version: " + Settings.SoftwareVersion);
            if (!CheckAndSetParameterBeforeTaskSchedulerActivity())
            {
                WriteToCloudFile("Host Connection fails");
                WriteToCloudFile("Action: " + actionprefx + " For Import Customer, Gift Cert. updates from Host" + " : end");
                return;
            }
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "StoreToCOExport.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\StoreToCOExport.exe";
            }


            if (Settings.OSVersion == "Win 10")
            {
                WriteToCloudFile("Try to set scheduler in Win 10");
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Retail Web Office GC Import".Replace("GwarePro", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Web Office GC Import".Replace("GwarePro", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  I|G";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtIgcTime.DateTime.Hour, (short)txtIgcTime.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    if (chkIgcRepeat.IsChecked == true)
                    {
                        SetScheduleEndTime(txtIgcTime, repi3);
                        int days = 0;
                        if (repi3.DateTime.Date > txtIgcTime.DateTime.Date) days = 1;
                        TimeSpan ets = new TimeSpan(days, repi3.DateTime.Hour, repi3.DateTime.Minute, repi3.DateTime.Second);
                        TimeSpan sts = new TimeSpan(txtIgcTime.DateTime.Hour, txtIgcTime.DateTime.Minute, txtIgcTime.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                        if (repi2.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(repi1.Value.ToString());
                        else dt.IntervalMinutes = GeneralFunctions.fnInt32(repi1.Value.ToString()) * 60;
                    }

                    t.Save();
                    t.Close();

                    if (btnIGCSchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnIGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnIGCSchedule.Tag = "Modify Task Scheduler";
                        btnIGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }

                    WriteToCloudFile("Scheduler successfully set");
                }
                catch (Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Import Customer, Gift Cert. updates from Host" + " : end");
            }


            if (Settings.OSVersion == "Win 11")
            {
                WriteToCloudFile("Try to set scheduler in Win 11");
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail Web Office GC Import".Replace("GwarePro", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtIgcTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtIgcTime.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkIgcRepeat.IsChecked == true)
                    {
                        SetScheduleEndTime(txtIgcTime, repi3);
                        int days = 0;
                        if (repi3.DateTime.Date > txtIgcTime.DateTime.Date) days = 1;
                        TimeSpan ets = new TimeSpan(days, repi3.DateTime.Hour, repi3.DateTime.Minute, repi3.DateTime.Second);
                        TimeSpan sts = new TimeSpan(txtIgcTime.DateTime.Hour, txtIgcTime.DateTime.Minute, txtIgcTime.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(repi2.SelectedIndex == 0 ? GeneralFunctions.fnInt32(repi1.Value.ToString()) : GeneralFunctions.fnInt32(repi1.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  I|G", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    if (btnIGCSchedule.Tag.ToString() == "Add Task Scheduler")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                        btnIGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnIGCSchedule.Tag = "Modify Task Scheduler";
                        btnIGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                    }

                    WriteToCloudFile("Scheduler successfully set");
                }
                catch(Exception ex)
                {
                    WriteToCloudFile(ex.Message);
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }

                WriteToCloudFile("Action: " + actionprefx + " For Import Customer, Gift Cert. updates from Host" + " : end");
            }

        }

        private void BtnIGCRunSchedule_Click(object sender, RoutedEventArgs e)
        {
            RunScheduler(SystemVariables.BrandName + " Retail Web Office GC Import".Replace("XEPOS", SystemVariables.BrandName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.XEPOS_Host_Setup;
            rep1.Visibility = Visibility.Hidden;
            rep2.Visibility = Visibility.Hidden;
            rep3.Visibility = Visibility.Hidden;
            rep4.Visibility = Visibility.Hidden;
            rep5.Visibility = Visibility.Hidden;
            txtgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 30, 0);
            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);

            repi1.Visibility = Visibility.Hidden;
            repi2.Visibility = Visibility.Hidden;
            repi3.Visibility = Visibility.Hidden;
            repi4.Visibility = Visibility.Hidden;
            repi5.Visibility = Visibility.Hidden;
            txtIgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 2, 30, 0);
            txtITime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 2, 0, 0);

            rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 20, 0, 0);
            repi3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 20, 0, 0);

            gcco1.IsEnabled = false;
            gcco2.IsEnabled = false;
            gcco3.IsEnabled = false;
            gcco4.IsEnabled = false;

            if (Settings.CentralCustomerID == 0)
            {
                chkCentral.IsChecked = false;
                btnClientLogout.Visibility = Visibility.Hidden;
                btnClientLogin.IsEnabled = true;
                txtClientCode.IsEnabled = txtClientPassword.IsEnabled = true;
            }
            else
            {
                chkCentral.IsChecked = true;
                btnClientLogout.Visibility = Visibility.Visible;
                btnClientLogin.IsEnabled = false;
                txtClientCode.IsEnabled = txtClientPassword.IsEnabled = false;
                GetCentralCustomerLoginDetails();

                
                
            }

            if (Settings.CentralExportImport == "N")
            {
                gcco1.IsEnabled = false;
                gcco2.IsEnabled = false;
            }
            else
            {
                gcco1.IsEnabled = true;
                gcco2.IsEnabled = true;
            }

            string strRegisteredModule = GeneralFunctions.RegisteredModules();
            chkRunScaleComController.Visibility = strRegisteredModule.Contains("SCALE") == true ? Visibility.Visible : Visibility.Hidden;
            if (!strRegisteredModule.Contains("SCALE")) chkRunScaleComController.IsChecked = false;
            ShowData();
            GetTaskScheduler();

            if ((Settings.CentralCustomerID > 0) &&  (txtStoreCode.Text.Trim() != "") && (txtStoreName.Text.Trim() != ""))
            {
                try
                {

                    bool f1 = false;
                    PosDataObject.Central ochkyyy = new PosDataObject.Central();
                    ochkyyy.Connection = SystemVariables.rmConn;
                    f1 = ochkyyy.IfExistStoreInWebOffice(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);

                    if (f1)
                    {
                        if (GeneralFunctions.fnInt32(Settings.strStoreHostId) == 0) // Update store with HostID
                        {
                            PosDataObject.Central ochk110 = new PosDataObject.Central();
                            ochk110.Connection = SystemVariables.rmConn;
                            int hostId = ochk110.GetStoreHostID(txtStoreCode.Text.Trim(), Settings.CentralCustomerID);
                            if (hostId > 0)
                            {
                                PosDataObject.Central ochk111 = new PosDataObject.Central();
                                ochk111.Connection = SystemVariables.Conn;
                                string retval = ochk111.UpdateStoreHostID(hostId);
                            }
                        }

                    }
                }
                catch
                {

                }
            }

            if (Settings.CentralCustomerID > 0)
            {
                chkCentral.IsEnabled = false;
            }
            boolControlChanged = false;
        }

        private void GetCentralCustomerLoginDetails()
        {
            if (SetCloudConn())
            {
                PosDataObject.Central objCentral = new PosDataObject.Central();
                objCentral.Connection = SystemVariables.rmConn;
                DataTable dtbl = new DataTable();
                dtbl = objCentral.GetCustomerInformationDetailFromWeb(Settings.CentralCustomerID);
                foreach (DataRow dr in dtbl.Rows)
                {
                    txtClientCode.Text = dr["CustCode"].ToString();
                    txtClientPassword.Text = dr["Password"].ToString();
                }
                dtbl.Dispose();
            }
        }

        private void ShowData()
        {
            if (Settings.CentralExportImport == "Y") chkCentral.IsChecked = true;
            else chkCentral.IsChecked = false;

            if (Settings.RunScaleControllerOnHostImport == "Y") chkRunScaleComController.IsChecked = true;
            else chkRunScaleComController.IsChecked = false;

            txtStoreCode.Text = Settings.StoreCode;
            txtStoreName.Text = Settings.StoreName;
        }

        private void GetTaskScheduler()
        {
            if (Settings.CentralExportImport == "Y")
            {
                if (Settings.OSVersion == "Win 10")
                {


                    try
                    {
                        ScheduledTasks st1 = new ScheduledTasks();
                        TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName));

                        foreach (TaskScheduler.DailyTrigger dt1 in t1.Triggers)
                        {
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                        }
                        btnSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnSchedule.Tag = "Modify Task Scheduler";
                        btnRunSchedule.Visibility = Visibility.Visible;
                    }
                    catch
                    {
                        btnSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnSchedule.Tag = "Add Task Scheduler";
                    }

                    try
                    {
                        ScheduledTasks st2 = new ScheduledTasks();
                        TaskScheduler.Task t2 = st2.OpenTask(SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName));
                        foreach (TaskScheduler.DailyTrigger dt2 in t2.Triggers)
                        {
                            txtgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                            if (dt2.IntervalMinutes > 0)
                            {
                                chkgcRepeat.IsChecked = true;
                                rep1.Visibility = Visibility.Visible;
                                rep2.Visibility = Visibility.Visible;
                                rep3.Visibility = Visibility.Visible;
                                rep4.Visibility = Visibility.Visible;
                                rep5.Visibility = Visibility.Visible;
                                int durmin = dt2.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                                rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    rep1.Value = quotient;
                                    rep2.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep1.Value = dt2.IntervalMinutes;
                                    rep2.SelectedIndex = 0;
                                }
                            }
                        }
                        btnGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnGCRunSchedule.Visibility = Visibility.Visible;
                        btnGCSchedule.Tag = "Modify Task Scheduler";
                    }
                    catch
                    {
                        btnGCSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnGCSchedule.Tag = "Add Task Scheduler";
                    }


                    try
                    {
                        ScheduledTasks st3 = new ScheduledTasks();
                        TaskScheduler.Task t3 = st3.OpenTask(SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName));

                        foreach (TaskScheduler.DailyTrigger dt3 in t3.Triggers)
                        {
                            txtITime.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt3.StartHour, dt3.StartMinute, 0);
                        }
                        btnISchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnISchedule.Tag = "Modify Task Scheduler";
                        btnIRunSchedule.Visibility = Visibility.Visible;
                    }
                    catch
                    {

                        btnISchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnISchedule.Tag = "Add Task Scheduler";
                    }

                    try
                    {
                        ScheduledTasks st4 = new ScheduledTasks();
                        TaskScheduler.Task t4 = st4.OpenTask(SystemVariables.BrandName + " Retail Web Office GC Import".Replace("GwarePro", SystemVariables.BrandName));
                        foreach (TaskScheduler.DailyTrigger dt4 in t4.Triggers)
                        {
                            txtIgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt4.StartHour, dt4.StartMinute, 0);
                            if (dt4.IntervalMinutes > 0)
                            {
                                chkIgcRepeat.IsChecked = true;
                                repi1.Visibility = Visibility.Visible;
                                repi2.Visibility = Visibility.Visible;
                                repi3.Visibility = Visibility.Visible;
                                repi4.Visibility = Visibility.Visible;
                                repi5.Visibility = Visibility.Visible;
                                int durmin = dt4.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt1 = new TimeSpan(dt4.StartHour + h, dt4.StartMinute + mi, 0);
                                repi3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt1.Hours, tt1.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt4.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    repi1.Value = quotient;
                                    repi2.SelectedIndex = 1;
                                }
                                else
                                {
                                    repi1.Value = dt4.IntervalMinutes;
                                    repi2.SelectedIndex = 0;
                                }
                            }
                        }
                        btnIGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                        btnIGCSchedule.Tag = "Modify Task Scheduler";
                        btnIGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    catch
                    {
                        btnIGCSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnIGCSchedule.Tag = "Add Task Scheduler";
                    }

                }

                if (Settings.OSVersion == "Win 11")
                {

                    try
                    {
                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Web Office General Export".Replace("XEPOS", SystemVariables.BrandName));
                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                            btnSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                            btnSchedule.Tag = "Modify Task Scheduler";
                            btnRunSchedule.Visibility = Visibility.Visible;
                        }


                    }
                    catch
                    {
                        btnSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnSchedule.Tag = "Add Task Scheduler";
                    }


                    try
                    {

                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Web Office General Import".Replace("XEPOS", SystemVariables.BrandName));
                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtITime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                            btnISchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                            btnISchedule.Tag = "Modify Task Scheduler";
                            btnIRunSchedule.Visibility = Visibility.Visible;
                        }



                    }
                    catch
                    {

                        btnISchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnISchedule.Tag = "Add Task Scheduler";
                    }



                    try
                    {
                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Web Office GC Export".Replace("XEPOS", SystemVariables.BrandName));


                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                            RepetitionPattern repetition = dt.Repetition;
                            if (repetition.Interval.TotalMinutes > 0)
                            {
                                chkgcRepeat.IsChecked = true;
                                rep1.Visibility = Visibility.Visible;
                                rep2.Visibility = Visibility.Visible;
                                rep3.Visibility = Visibility.Visible;
                                rep4.Visibility = Visibility.Visible;
                                rep5.Visibility = Visibility.Visible;
                                int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                                rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                                if (val == 0)
                                {
                                    rep1.Value = quotient;
                                    rep2.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep1.Value = repetition.Interval.Minutes;
                                    rep2.SelectedIndex = 0;
                                }
                            }

                            btnGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                            btnGCRunSchedule.Visibility = Visibility.Visible;
                            btnGCSchedule.Tag = "Modify Task Scheduler";
                        }



                    }
                    catch
                    {
                        btnGCSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnGCSchedule.Tag = "Add Task Scheduler";
                    }



                    try
                    {
                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Web Office GC Import".Replace("XEPOS", SystemVariables.BrandName));


                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtIgcTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                            RepetitionPattern repetition = dt.Repetition;
                            if (repetition.Interval.TotalMinutes > 0)
                            {
                                chkIgcRepeat.IsChecked = true;
                                repi1.Visibility = Visibility.Visible;
                                repi2.Visibility = Visibility.Visible;
                                repi3.Visibility = Visibility.Visible;
                                repi4.Visibility = Visibility.Visible;
                                repi5.Visibility = Visibility.Visible;
                                int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                                repi3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                                if (val == 0)
                                {
                                    repi1.Value = quotient;
                                    repi2.SelectedIndex = 1;
                                }
                                else
                                {
                                    repi1.Value = repetition.Interval.Minutes;
                                    repi2.SelectedIndex = 0;
                                }
                            }

                            btnIGCSchedule.Content = Properties.Resources.Modify_Task_Scheduler.ToUpper();
                            btnIGCSchedule.Tag = "Modify Task Scheduler";
                            btnIGCRunSchedule.Visibility = Visibility.Visible;
                        }



                    }
                    catch
                    {
                        btnIGCSchedule.Content = Properties.Resources.Set_Task_Scheduler.ToUpper();
                        btnIGCSchedule.Tag = "Add Task Scheduler";
                    }

                }
                
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (ValidAll())
                {
                    if (SaveData())
                    {
                        Settings.LoadSettingsVariables();
                        if (Settings.DefautInternational == "N")
                            GeneralFunctions.SetCurrencyNDateformat();
                        boolControlChanged = false;
                        CloseKeyboards();
                        DialogResult = true;
                    }
                }
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            DialogResult = false;
        }

        private bool SaveData()
        {
            int StoreExists = GeneralFunctions.GetRecordCount("CentralExportImport");
             
            int mystoreId =   Settings.strStoreId== "" ? 0 : Convert.ToInt32(Settings.strStoreId); //GeneralFunctions.GetStoreID("CentralExportImport", txtStoreCode.Text.Trim());
            string err = "";

            PosDataObject.Setup objsetup = new PosDataObject.Setup();
            objsetup.Connection = SystemVariables.Conn;

            if (chkCentral.IsChecked == true)
            {
                if (StoreExists == 0)
                {
                    try
                    {
                        err = objsetup.AddStoreCodeForGwareHost(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());
                    }
                    catch
                    {
                    }
                }
                else
                {
                    try
                    {
                        err = objsetup.UpdateStoreCodeForGwareHostWithId(mystoreId, txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());
                       // err = objsetup.UpdateStoreCodeForGwareHost(txtStoreCode.Text.Trim(), txtStoreName.Text.Trim());
                    }
                    catch
                    {
                    }
                }
            }

            /*
            if (err == "")
            {
                try
                {
                    err = objsetup.UpdateGwareHostSetup(chkCentral.Checked? "Y" : "N", txtcldSvr.Text.Trim(), txtcldDb.Text.Trim(), txtClientCode.Text.Trim(), txtClientPassword.Text.Trim(),chkRunScaleComController.Checked ? "Y" : "N");
                }
                catch
                {
                }
            }*/

            if (chkCentral.IsChecked ==  true)
            {
                err = objsetup.UpdateGiftCertStore(txtStoreCode.Text.Trim());
                err = objsetup.UpdateCustomerStore(txtStoreCode.Text.Trim());
                err = objsetup.UpdateCustomerAcRecvStore(txtStoreCode.Text.Trim());
                err = objsetup.UpdateGeneralMappingStore(txtStoreCode.Text.Trim());
                err = objsetup.UpdateEmployeeStore(txtStoreCode.Text.Trim());
            }



            Settings.StoreCode = txtStoreCode.Text;
            Settings.StoreName = txtStoreName.Text;
            Settings.CentralExportImport = chkCentral.IsChecked ==  true ? "Y" : "N";
            //Settings.CloudServer = txtcldSvr.Text;
            //Settings.CloudDB = txtcldDb.Text;
            //Settings.CloudUser = txtClientCode.Text;
            //Settings.CloudPassword = txtClientPassword.Text;
            return true;
        }

        private bool ValidAll()
        {

            if (chkCentral.IsChecked == true)
            {
                if (Settings.CentralCustomerID == 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_login_to_XEPOS_Account);
                    GeneralFunctions.SetFocus(txtClientCode);
                    return false;
                }
                if (txtStoreCode.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Store_Code);
                    GeneralFunctions.SetFocus(txtStoreCode);
                    return false;
                }

                if (txtStoreName.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Store_Name);
                    GeneralFunctions.SetFocus(txtStoreName);
                    return false;
                }

                if (!IsActiveCentralCustomer())
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_login_with_a_valid_XEPOS_Account);
                    GeneralFunctions.SetFocus(txtClientCode);
                    return false;
                }
            }

            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (ValidAll())
                    {
                        if (SaveData())
                        {
                            Settings.LoadSettingsVariables();
                            if (Settings.DefautInternational == "N")
                                GeneralFunctions.SetCurrencyNDateformat();
                            boolControlChanged = false;
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                if (DlgResult == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        private void Rep2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
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

        private void Full_GotFocusPswd(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).PointToScreen(new Point(0, 0));
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

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        #region Log

        private string LogFileName()
        {
            return "log_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + "_"
                   + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + ".txt";
        }

        private string LogFilePath()
        {
            string strfilename = "";
            string strdirpath = "";
            string userpath = "";


            userpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);//System.AppDomain.CurrentDomain.BaseDirectory;

            // userpath = Assembly.GetExecutingAssembly().Location;

            //  userpath = userpath.Replace("SchedulerSetup.exe", "");
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\Logs";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\Logs";
            }

            if (!System.IO.Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
                strfilename = userpath + "\\" + logF;
            }
            else
            {
                strfilename = userpath + "\\" + logF;
            }


            logP = strfilename;
            return strfilename;
        }

        private void WriteToLogFile(string txt)
        {
            if (logF == "") logF = LogFileName();
            string logP = LogFilePath();

            FileStream fileStrm;
            if (System.IO.File.Exists(logP)) fileStrm = new FileStream(logP, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(logP, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }




        private string LogFileNameCloud()
        {
            return DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + ".txt";
        }

        private string LogFilePathCloud()
        {
            string strfilename = "";
            string strdirpath = "";
            string userpath = "";


            userpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

           
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\Cloud";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\Cloud";
            }

            if (!System.IO.Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
                strfilename = userpath + "\\" + logCF;
            }
            else
            {
                strfilename = userpath + "\\" + logCF;
            }


            logCP = strfilename;
            return strfilename;
        }

        private void WriteToCloudFile(string txt)
        {
            if (logCF == "") logCF = LogFileNameCloud();
            string logCP = LogFilePathCloud();

            FileStream fileStrm;
            if (System.IO.File.Exists(logCP)) fileStrm = new FileStream(logCP, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(logCP, FileMode.OpenOrCreate, FileAccess.Write);

            string datetimeprefix = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + "-"
                  + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

            if (txt != "") txt = datetimeprefix + " : " + txt;
            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }

        #endregion
    }
}
