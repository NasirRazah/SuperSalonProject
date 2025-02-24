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
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_PrinterTemplateActivationDlg.xaml
    /// </summary>
    public partial class frm_PrinterTemplateActivationDlg : Window
    {
        FullKeyboard fkybrd;
       
       

        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

       

        public frm_PrinterTemplateActivationDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }


      

       

        private bool IsValidAll()
        {
            if (cmbTemplateType1.EditValue != null)
            {
                if (cmbPrinter1.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Receipt Template Printer");
                    cmbPrinter1.Focus();
                    return false;
                }
            }

            if (cmbTemplateType2.EditValue != null)
            {
                if (cmbPrinter2.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Layaway Template Printer");
                    cmbPrinter2.Focus();
                    return false;
                }
            }

            if (cmbTemplateType3.EditValue != null)
            {
                if (cmbPrinter3.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Rent Item Issue Template Printer");
                    cmbPrinter3.Focus();
                    return false;
                }
            }

            if (cmbTemplateType4.EditValue != null)
            {
                if (cmbPrinter4.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Rent Item Return Template Printer");
                    cmbPrinter4.Focus();
                    return false;
                }
            }

            if (cmbTemplateType5.EditValue != null)
            {
                if (cmbPrinter5.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Repair Item Receive Template Printer");
                    cmbPrinter5.Focus();
                    return false;
                }
            }

            if (cmbTemplateType6.EditValue != null)
            {
                if (cmbPrinter6.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Repair Item Return Template Printer");
                    cmbPrinter6.Focus();
                    return false;
                }
            }

            if (cmbTemplateType7.EditValue != null)
            {
                if (cmbPrinter7.EditValue == null)
                {
                    DocMessage.MsgInformation("Select WorkOrder Template Printer");
                    cmbPrinter7.Focus();
                    return false;
                }
            }

            if (cmbTemplateType8.EditValue != null)
            {
                if (cmbPrinter8.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Suspend Receipt Template Printer");
                    cmbPrinter8.Focus();
                    return false;
                }
            }

            if (cmbTemplateType9.EditValue != null)
            {
                if (cmbPrinter9.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Closeout Template Printer");
                    cmbPrinter9.Focus();
                    return false;
                }
            }

            if (cmbTemplateType10.EditValue != null)
            {
                if (cmbPrinter10.EditValue == null)
                {
                    DocMessage.MsgInformation("Select No Sale Template Printer");
                    cmbPrinter10.Focus();
                    return false;
                }
            }

            if (cmbTemplateType11.EditValue != null)
            {
                if (cmbPrinter11.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Paid Out Template Printer");
                    cmbPrinter11.Focus();
                    return false;
                }
            }

            if (cmbTemplateType12.EditValue != null)
            {
                if (cmbPrinter12.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Paid In Template Printer");
                    cmbPrinter12.Focus();
                    return false;
                }
            }

            if (cmbTemplateType13.EditValue != null)
            {
                if (cmbPrinter13.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Safe Drop Template Printer");
                    cmbPrinter13.Focus();
                    return false;
                }
            }

            if (cmbTemplateType14.EditValue != null)
            {
                if (cmbPrinter14.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Lotto Payout Template Printer");
                    cmbPrinter14.Focus();
                    return false;
                }
            }

            if (cmbTemplateType15.EditValue != null)
            {
                if (cmbPrinter15.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Customer Label Template Printer");
                    cmbPrinter15.Focus();
                    return false;
                }
            }

            if (cmbTemplateType16.EditValue != null)
            {
                if (cmbPrinter16.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Gift Receipt Template Printer");
                    cmbPrinter16.Focus();
                    return false;
                }
            }

            if (cmbTemplateType17.EditValue != null)
            {
                if (cmbPrinter17.EditValue == null)
                {
                    DocMessage.MsgInformation("Select Gift Aid Receipt Template Printer");
                    cmbPrinter16.Focus();
                    return false;
                }
            }

            if (cmbPrinter1.EditValue != null)
            {
                if (cmbTemplateType1.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Receipt Template");
                    //cmbTemplateType1.Focus();
                    //return false;
                    cmbPrinter1.EditValue = null;
                }
            }

            if (cmbPrinter2.EditValue != null)
            {
                if (cmbTemplateType2.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Layaway Template");
                    //cmbTemplateType2.Focus();
                    //return false;
                    cmbPrinter2.EditValue = null;
                }
            }

            if (cmbPrinter3.EditValue != null)
            {
                if (cmbTemplateType3.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Rent Issue Template");
                    //cmbTemplateType3.Focus();
                    //return false;
                    cmbPrinter3.EditValue = null;
                }
            }

            if (cmbPrinter4.EditValue != null)
            {
                if (cmbTemplateType4.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Rent Return Template");
                    //cmbTemplateType4.Focus();
                    //return false;
                    cmbPrinter4.EditValue = null;
                }
            }

            if (cmbPrinter5.EditValue != null)
            {
                if (cmbTemplateType5.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Repair In Template");
                    //cmbTemplateType5.Focus();
                    //return false;
                    cmbPrinter5.EditValue = null;
                }
            }


            if (cmbPrinter6.EditValue != null)
            {
                if (cmbTemplateType6.EditValue == null)
                {
                    // DocMessage.MsgInformation("Select Repair Deliver Template");
                    //cmbTemplateType6.Focus();
                    // return false;
                    cmbPrinter6.EditValue = null;
                }
            }

            if (cmbPrinter7.EditValue != null)
            {
                if (cmbTemplateType7.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select WorkOrder Template");
                    // cmbTemplateType7.Focus();
                    // return false;
                    cmbPrinter7.EditValue = null;
                }
            }

            if (cmbPrinter8.EditValue != null)
            {
                if (cmbTemplateType8.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Suspend Receipt Template");
                    //cmbTemplateType8.Focus();
                    //return false;
                    cmbPrinter8.EditValue = null;
                }
            }

            if (cmbPrinter9.EditValue != null)
            {
                if (cmbTemplateType9.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Closeout Template");
                    // cmbTemplateType9.Focus();
                    // return false;
                    cmbPrinter9.EditValue = null;
                }
            }

            if (cmbPrinter10.EditValue != null)
            {
                if (cmbTemplateType10.EditValue == null)
                {
                    // DocMessage.MsgInformation("Select No Sale Template");
                    // cmbTemplateType10.Focus();
                    //  return false;
                    cmbPrinter10.EditValue = null;
                }
            }

            if (cmbPrinter11.EditValue != null)
            {
                if (cmbTemplateType11.EditValue == null)
                {
                    //  DocMessage.MsgInformation("Select Paid Out Template");
                    // cmbTemplateType11.Focus();
                    //  return false;
                    cmbPrinter11.EditValue = null;
                }
            }

            if (cmbPrinter12.EditValue != null)
            {
                if (cmbTemplateType12.EditValue == null)
                {
                    // DocMessage.MsgInformation("Select Paid In Template");
                    // cmbTemplateType12.Focus();
                    // return false;
                    cmbPrinter12.EditValue = null;
                }
            }

            if (cmbPrinter13.EditValue != null)
            {
                if (cmbTemplateType13.EditValue == null)
                {
                    //  DocMessage.MsgInformation("Select Safe Drop Template");
                    //  cmbTemplateType13.Focus();
                    //   return false;
                    cmbPrinter13.EditValue = null;
                }
            }

            if (cmbPrinter14.EditValue != null)
            {
                if (cmbTemplateType14.EditValue == null)
                {
                    //  DocMessage.MsgInformation("Select Lotto Payout Template");
                    //  cmbTemplateType14.Focus();
                    //  return false;
                    cmbPrinter14.EditValue = null;
                }
            }

            if (cmbPrinter15.EditValue != null)
            {
                if (cmbTemplateType15.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Customer Label Template");
                    // cmbTemplateType15.Focus();
                    //return false;
                    cmbPrinter15.EditValue = null;
                }
            }

            if (cmbPrinter16.EditValue != null)
            {
                if (cmbTemplateType16.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Customer Label Template");
                    // cmbTemplateType15.Focus();
                    //return false;
                    cmbPrinter16.EditValue = null;
                }
            }

            if (cmbPrinter17.EditValue != null)
            {
                if (cmbTemplateType17.EditValue == null)
                {
                    //DocMessage.MsgInformation("Select Customer Label Template");
                    // cmbTemplateType15.Focus();
                    //return false;
                    cmbPrinter17.EditValue = null;
                }
            }

            return true;
        }

       

        private int GetMaxPaymentOrder()
        {
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            return objTenderTypes.MaxPaymentOrder();
        }

       

       

        private void TxtName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            SetLocalPrinter1();
                            SetLocalPrinter2();
                            SetLocalPrinter3();
                            SetLocalPrinter4();
                            SetLocalPrinter5();
                            SetLocalPrinter6();
                            SetLocalPrinter7();
                            SetLocalPrinter8();
                            SetLocalPrinter9();
                            SetLocalPrinter10();
                            SetLocalPrinter11();
                            SetLocalPrinter12();
                            SetLocalPrinter13();
                            SetLocalPrinter14();
                            SetLocalPrinter15();
                            SetLocalPrinter16();
                            SetLocalPrinter17();

                            Settings.GetCustomTemplatePrinters();

                            boolControlChanged = false;
                            
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTemplateType1.ItemsSource = GetActiveList("Receipt");
            cmbTemplateType2.ItemsSource = GetActiveList("Layaway");
            cmbTemplateType3.ItemsSource = GetActiveList("Rent Item Issue");
            cmbTemplateType4.ItemsSource = GetActiveList("Rent Item Return");
            cmbTemplateType5.ItemsSource = GetActiveList("Repair Item Receive");
            cmbTemplateType6.ItemsSource = GetActiveList("Repair Item Return");
            cmbTemplateType7.ItemsSource = GetActiveList("WorkOrder");
            cmbTemplateType8.ItemsSource = GetActiveList("Suspend Receipt");
            cmbTemplateType9.ItemsSource = GetActiveList("Closeout");
            cmbTemplateType10.ItemsSource = GetActiveList("No Sale");
            cmbTemplateType11.ItemsSource = GetActiveList("Paid Out");
            cmbTemplateType12.ItemsSource = GetActiveList("Paid In");
            cmbTemplateType13.ItemsSource = GetActiveList("Safe Drop");
            cmbTemplateType14.ItemsSource = GetActiveList("Lotto Payout");
            cmbTemplateType15.ItemsSource = GetActiveList("Customer Label");
            cmbTemplateType16.ItemsSource = GetActiveList("Gift Receipt");
            cmbTemplateType17.ItemsSource = GetActiveList("Gift Aid Receipt");


            cmbPrinter1.Items.Clear();
            cmbPrinter2.Items.Clear();
            cmbPrinter3.Items.Clear();
            cmbPrinter4.Items.Clear();
            cmbPrinter5.Items.Clear();
            cmbPrinter6.Items.Clear();
            cmbPrinter7.Items.Clear();
            cmbPrinter8.Items.Clear();
            cmbPrinter9.Items.Clear();
            cmbPrinter10.Items.Clear();
            cmbPrinter11.Items.Clear();
            cmbPrinter12.Items.Clear();
            cmbPrinter13.Items.Clear();
            cmbPrinter14.Items.Clear();
            cmbPrinter15.Items.Clear();
            cmbPrinter16.Items.Clear();
            cmbPrinter17.Items.Clear();

            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter1.Items.Add(strPrinter);
                cmbPrinter2.Items.Add(strPrinter);
                cmbPrinter3.Items.Add(strPrinter);
                cmbPrinter4.Items.Add(strPrinter);
                cmbPrinter5.Items.Add(strPrinter);
                cmbPrinter6.Items.Add(strPrinter);
                cmbPrinter7.Items.Add(strPrinter);
                cmbPrinter8.Items.Add(strPrinter);
                cmbPrinter9.Items.Add(strPrinter);
                cmbPrinter10.Items.Add(strPrinter);
                cmbPrinter11.Items.Add(strPrinter);
                cmbPrinter12.Items.Add(strPrinter);
                cmbPrinter13.Items.Add(strPrinter);
                cmbPrinter14.Items.Add(strPrinter);
                cmbPrinter15.Items.Add(strPrinter);
                cmbPrinter16.Items.Add(strPrinter);
                cmbPrinter17.Items.Add(strPrinter);
            }

            cmbPrinter1.SelectedIndex = cmbPrinter1.Items.IndexOf(Settings.CustomTemplatePrinter1);
            cmbPrinter2.SelectedIndex = cmbPrinter2.Items.IndexOf(Settings.CustomTemplatePrinter2);
            cmbPrinter3.SelectedIndex = cmbPrinter3.Items.IndexOf(Settings.CustomTemplatePrinter3);
            cmbPrinter4.SelectedIndex = cmbPrinter4.Items.IndexOf(Settings.CustomTemplatePrinter4);
            cmbPrinter5.SelectedIndex = cmbPrinter5.Items.IndexOf(Settings.CustomTemplatePrinter5);
            cmbPrinter6.SelectedIndex = cmbPrinter6.Items.IndexOf(Settings.CustomTemplatePrinter6);
            cmbPrinter7.SelectedIndex = cmbPrinter7.Items.IndexOf(Settings.CustomTemplatePrinter7);
            cmbPrinter8.SelectedIndex = cmbPrinter8.Items.IndexOf(Settings.CustomTemplatePrinter8);
            cmbPrinter9.SelectedIndex = cmbPrinter9.Items.IndexOf(Settings.CustomTemplatePrinter9);
            cmbPrinter10.SelectedIndex = cmbPrinter10.Items.IndexOf(Settings.CustomTemplatePrinter10);
            cmbPrinter11.SelectedIndex = cmbPrinter11.Items.IndexOf(Settings.CustomTemplatePrinter11);
            cmbPrinter12.SelectedIndex = cmbPrinter12.Items.IndexOf(Settings.CustomTemplatePrinter12);
            cmbPrinter13.SelectedIndex = cmbPrinter13.Items.IndexOf(Settings.CustomTemplatePrinter13);
            cmbPrinter14.SelectedIndex = cmbPrinter14.Items.IndexOf(Settings.CustomTemplatePrinter14);
            cmbPrinter15.SelectedIndex = cmbPrinter15.Items.IndexOf(Settings.CustomTemplatePrinter15);
            cmbPrinter16.SelectedIndex = cmbPrinter15.Items.IndexOf(Settings.CustomTemplatePrinter16);
            cmbPrinter17.SelectedIndex = cmbPrinter15.Items.IndexOf(Settings.CustomTemplatePrinter17);

            int t1 = 0;
            int t2 = 0;
            int t3 = 0;
            int t4 = 0;
            int t5 = 0;
            int t6 = 0;
            int t7 = 0;
            int t8 = 0;
            int t9 = 0;
            int t10 = 0;
            int t11 = 0;
            int t12 = 0;
            int t13 = 0;
            int t14 = 0;
            int t15 = 0;
            int t16 = 0;
            int t17 = 0;

            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;

            DataTable dtbl = obj.FetchActiveTemplateValue();
            foreach(DataRow dr in dtbl.Rows)
            {
                t1 = GeneralFunctions.fnInt32(dr["T1"].ToString());
                t2 = GeneralFunctions.fnInt32(dr["T2"].ToString());
                t3 = GeneralFunctions.fnInt32(dr["T3"].ToString());
                t4 = GeneralFunctions.fnInt32(dr["T4"].ToString());
                t5 = GeneralFunctions.fnInt32(dr["T5"].ToString());
                t6 = GeneralFunctions.fnInt32(dr["T6"].ToString());
                t7 = GeneralFunctions.fnInt32(dr["T7"].ToString());
                t8 = GeneralFunctions.fnInt32(dr["T8"].ToString());
                t9 = GeneralFunctions.fnInt32(dr["T9"].ToString());
                t10 = GeneralFunctions.fnInt32(dr["T10"].ToString());
                t11 = GeneralFunctions.fnInt32(dr["T11"].ToString());
                t12 = GeneralFunctions.fnInt32(dr["T12"].ToString());
                t13 = GeneralFunctions.fnInt32(dr["T13"].ToString());
                t14 = GeneralFunctions.fnInt32(dr["T14"].ToString());
                t15 = GeneralFunctions.fnInt32(dr["T15"].ToString());
                t16 = GeneralFunctions.fnInt32(dr["T16"].ToString());
                t17 = GeneralFunctions.fnInt32(dr["T17"].ToString());
            }
            dtbl.Dispose();

            cmbTemplateType1.EditValue = t1.ToString();
            cmbTemplateType2.EditValue = t2.ToString();
            cmbTemplateType3.EditValue = t3.ToString();
            cmbTemplateType4.EditValue = t4.ToString();
            cmbTemplateType5.EditValue = t5.ToString();
            cmbTemplateType6.EditValue = t6.ToString();
            cmbTemplateType7.EditValue = t7.ToString();
            cmbTemplateType8.EditValue = t8.ToString();
            cmbTemplateType9.EditValue = t9.ToString();
            cmbTemplateType10.EditValue = t10.ToString();
            cmbTemplateType11.EditValue = t11.ToString();
            cmbTemplateType12.EditValue = t12.ToString();
            cmbTemplateType13.EditValue = t13.ToString();
            cmbTemplateType14.EditValue = t14.ToString();
            cmbTemplateType15.EditValue = t15.ToString();
            cmbTemplateType16.EditValue = t16.ToString();
            cmbTemplateType17.EditValue = t17.ToString();
            boolControlChanged = false;
        }

        private DataTable GetActiveList(string ttype)
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            return obj.FetchActiveTemplateList(ttype);
         }

        private bool SaveData()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            bool blexists = obj.IfExistsActiveTemplate();
            obj.LoginUserID = SystemVariables.CurrentUserID;
            obj.ActiveTemplate1 = cmbTemplateType1.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType1.EditValue);
            obj.ActiveTemplate2 = cmbTemplateType2.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType2.EditValue);
            obj.ActiveTemplate3 = cmbTemplateType3.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType3.EditValue);
            obj.ActiveTemplate4 = cmbTemplateType4.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType4.EditValue);
            obj.ActiveTemplate5 = cmbTemplateType5.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType5.EditValue);
            obj.ActiveTemplate6 = cmbTemplateType6.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType6.EditValue);
            obj.ActiveTemplate7 = cmbTemplateType7.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType7.EditValue);
            obj.ActiveTemplate8 = cmbTemplateType8.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType8.EditValue);
            obj.ActiveTemplate9 = cmbTemplateType9.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType9.EditValue);
            obj.ActiveTemplate10 = cmbTemplateType10.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType10.EditValue);
            obj.ActiveTemplate11 = cmbTemplateType11.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType11.EditValue);
            obj.ActiveTemplate12 = cmbTemplateType12.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType12.EditValue);
            obj.ActiveTemplate13 = cmbTemplateType13.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType13.EditValue);
            obj.ActiveTemplate14 = cmbTemplateType14.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType14.EditValue);
            obj.ActiveTemplate15 = cmbTemplateType15.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType15.EditValue);
            obj.ActiveTemplate16 = cmbTemplateType16.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType16.EditValue);
            obj.ActiveTemplate17 = cmbTemplateType17.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbTemplateType17.EditValue);

            string err = "";
            if (!blexists) err = obj.InsertActiveData();
            else err = obj.UpdateActiveData();
            return err == "";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    SetLocalPrinter1();
                    SetLocalPrinter2();
                    SetLocalPrinter3();
                    SetLocalPrinter4();
                    SetLocalPrinter5();
                    SetLocalPrinter6();
                    SetLocalPrinter7();
                    SetLocalPrinter8();
                    SetLocalPrinter9();
                    SetLocalPrinter10();
                    SetLocalPrinter11();
                    SetLocalPrinter12();
                    SetLocalPrinter13();
                    SetLocalPrinter14();
                    SetLocalPrinter15();
                    SetLocalPrinter16();
                    SetLocalPrinter17();
                    Settings.GetCustomTemplatePrinters();
                    boolControlChanged = false;
                    Close();
                }
            }
        }


        
        private void SetLocalPrinter1()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 1");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 1";
            objLsetup.ParamValue = cmbPrinter1.Text == null ? "" : cmbPrinter1.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter2()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 2");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 2";
            objLsetup.ParamValue = cmbPrinter2.Text == null ? "" : cmbPrinter2.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter3()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 3");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 3";
            objLsetup.ParamValue = cmbPrinter3.Text == null ? "" : cmbPrinter3.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter4()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 4");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 4";
            objLsetup.ParamValue = cmbPrinter4.Text == null ? "" : cmbPrinter4.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter5()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 5");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 5";
            objLsetup.ParamValue = cmbPrinter5.Text == null ? "" : cmbPrinter5.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter6()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 6");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 6";
            objLsetup.ParamValue = cmbPrinter6.Text == null ? "" : cmbPrinter6.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter7()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 7");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 7";
            objLsetup.ParamValue = cmbPrinter7.Text == null ? "" : cmbPrinter7.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter8()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 8");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 8";
            objLsetup.ParamValue = cmbPrinter8.Text == null ? "" : cmbPrinter8.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter9()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 9");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 9";
            objLsetup.ParamValue = cmbPrinter9.Text == null ? "" : cmbPrinter9.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter10()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 10");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 10";
            objLsetup.ParamValue = cmbPrinter10.Text == null ? "" : cmbPrinter10.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter11()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 11");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 11";
            objLsetup.ParamValue = cmbPrinter11.Text == null ? "" : cmbPrinter11.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter12()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 12");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 12";
            objLsetup.ParamValue = cmbPrinter12.Text == null ? "" : cmbPrinter12.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter13()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 13");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 13";
            objLsetup.ParamValue = cmbPrinter13.Text == null ? "" : cmbPrinter13.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter14()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 14");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 14";
            objLsetup.ParamValue = cmbPrinter14.Text == null ? "" : cmbPrinter14.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter15()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 15");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 15";
            objLsetup.ParamValue = cmbPrinter15.Text == null ? "" : cmbPrinter15.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter16()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 16");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 16";
            objLsetup.ParamValue = cmbPrinter16.Text == null ? "" : cmbPrinter16.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLocalPrinter17()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Custom Template Printer 17");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Custom Template Printer 17";
            objLsetup.ParamValue = cmbPrinter17.Text == null ? "" : cmbPrinter17.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkEnabled_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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





     

       

       
        

       

        private void CmbTemplateType1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbPrinter1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
