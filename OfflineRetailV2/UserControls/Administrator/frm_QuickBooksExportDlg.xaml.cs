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
using System.IO;
using Microsoft.Win32;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_QuickBooksExportDlg.xaml
    /// </summary>
    public partial class frm_QuickBooksExportDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_QuickBooksExportDlg()
        {
            InitializeComponent();
             ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private int CLSC = 0;

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
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

        public void PopulateEmployee()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            objEmployee.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblEmp = new DataTable();
            dbtblEmp = objEmployee.FetchModifiedLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblEmp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbEmployee.ItemsSource = dtblTemp;
            
            int SelectID = 0;
            foreach (DataRow dr in dbtblEmp.Rows)
            {
                SelectID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                break;
            }
            cmbEmployee.EditValue = 0;
            dbtblEmp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            PopulateEmployee();
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            cmbExport.SelectedIndex = 1;
            rbTab.IsChecked = true;
            txtChar.IsEnabled = false;
        }

        private void BtnOK_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (ValidAll())
                {
                    CLSC = 0;
                    int rtn = 0;
                    if (cmbExport.SelectedIndex == 0) rtn = ExecuteCloseoutExport();
                    if (cmbExport.SelectedIndex == 1) rtn = ExecutePayrollExport();
                    if (rtn == 0)
                    {
                        if ((CLSC > 1) && (cmbExport.SelectedIndex == 0))
                        {
                            DocMessage.MsgInformation(Properties.Resources.Export_completed_successfully + " " + "" + CLSC.ToString() + " " + Properties.Resources.files_generated);
                        }
                        else
                        {
                            DocMessage.MsgInformation(Properties.Resources.Export_completed_successfully);
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = mem_location.Text;
                            p.Start();
                            CloseKeyboards();
                            Close();
                        }
                    }
                    if (rtn == 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.No_data_found_for_Export);
                    }
                    if (rtn == 2)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Permission_error_while_exporting);
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
            CloseKeyboards();
            Close();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Export File";
            op.Filter = "txt files(*.csv) | (*.txt) | All files(*.*) | *.* ";
            op.DefaultExt = "csv";
            op.CheckFileExists = false;
            op.FilterIndex = 1;
            op.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (cmbExport.SelectedIndex == 0) op.FileName = DateTime.Now.ToString("MMddyy_HHmm") + "_RWqbexport_C" + ".csv";
            if (cmbExport.SelectedIndex == 1) op.FileName = DateTime.Now.ToString("MMddyy_HHmm") + "_emp_hour" + ".csv";
            if (op.ShowDialog() == true)
            {
                string strFilename = "";
                int intIndex = 0;
                strFilename = op.FileName;
                intIndex = strFilename.IndexOf(".");
                if (intIndex <= 0)
                    strFilename = strFilename + ".csv";
                mem_location.Text = strFilename;
            }   
        }

        private void CmbExport_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbExport.SelectedIndex == 0)
            {
                lbEmployee.Visibility = Visibility.Hidden;
                cmbEmployee.Visibility = Visibility.Hidden;
            }
            if (cmbExport.SelectedIndex == 1)
            {
                lbEmployee.Visibility = Visibility.Visible;
                cmbEmployee.Visibility = Visibility.Visible;
            }
        }

        private string GetSeparator()
        {
            if (rbComma.IsChecked == true) return ",";
            else if (rbTab.IsChecked == true) return "\t";
            else if (rbSpace.IsChecked == true) return " ";
            else return txtChar.Text;
        }

        private void RbComma_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCustom.IsChecked == true)
            {
                txtChar.IsEnabled = true;
            }
            else
            {
                txtChar.IsEnabled = false;
            }
        }

        private int ExecuteCloseoutExport()
        {
            int rtn = 0;
            DataTable dtbl = new DataTable();
            DataTable dtblH = new DataTable();
            DataTable dtblT = new DataTable();
            DataTable dtblD = new DataTable();
            int intExpID = 0;
            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutExport(dtFrom.DateTime, dtTo.DateTime, Settings.TerminalName);



            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CO_ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CO_StartDate", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CO_EndDate", System.Type.GetType("System.String"));
            dtbl.Columns.Add("RWAccount", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));

            PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
            objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblH = objCloseout3.ShowHeaderRecord(Settings.TerminalName,SystemVariables.DateFormat);



            foreach (DataRow dr in dtblH.Rows)
            {

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "SALES",
                                            ""});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Product Sales",
                                            GeneralFunctions.fnDouble(dr["ProductSales"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Service Sales",
                                            GeneralFunctions.fnDouble(dr["ServiceSales"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Other Sales",
                                            GeneralFunctions.fnDouble(dr["OtherSales"].ToString()).ToString("f")});

                if ((dr["Tax1Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["Tax1Amount"].ToString()) != 0))
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax1Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["Tax1Amount"].ToString()).ToString("f")});
                }
                if ((dr["Tax2Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["Tax2Amount"].ToString()) != 0))
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax2Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["Tax2Amount"].ToString()).ToString("f")});
                }

                if ((dr["Tax3Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["Tax3Amount"].ToString()) != 0))
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax3Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["Tax3Amount"].ToString()).ToString("f")});
                }


                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Total Sales",
                                           (GeneralFunctions.fnDouble(dr["ProductSales"].ToString()) + GeneralFunctions.fnDouble(dr["ServiceSales"].ToString())
                                          + GeneralFunctions.fnDouble(dr["OtherSales"].ToString()) + GeneralFunctions.fnDouble(dr["Tax1Amount"].ToString()) +
                                            GeneralFunctions.fnDouble(dr["Tax2Amount"].ToString()) + GeneralFunctions.fnDouble(dr["Tax3Amount"].ToString())).ToString("f")});


                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Taxable Sales",
                                           (GeneralFunctions.fnDouble(dr["ProductTx"].ToString()) + GeneralFunctions.fnDouble(dr["ServiceTx"].ToString())
                                          + GeneralFunctions.fnDouble(dr["OtherTx"].ToString())).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Non Taxable Sales",
                                           (GeneralFunctions.fnDouble(dr["ProductNTx"].ToString()) + GeneralFunctions.fnDouble(dr["ServiceNTx"].ToString())
                                          + GeneralFunctions.fnDouble(dr["OtherNTx"].ToString())).ToString("f")});

                if ((GeneralFunctions.fnDouble(dr["RentSales"].ToString()) != 0) || (GeneralFunctions.fnDouble(dr["RentDeposit"].ToString()) != 0)
                    || (GeneralFunctions.fnDouble(dr["RentDepositReturned"].ToString()) != 0))
                {

                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "RENT",
                                            ""});

                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Rent Sales",
                                            GeneralFunctions.fnDouble(dr["RentSales"].ToString()).ToString("f")});

                    if ((dr["Tax1Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RentTax1Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax1Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RentTax1Amount"].ToString()).ToString("f")});
                    }
                    if ((dr["Tax2Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RentTax2Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax2Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RentTax2Amount"].ToString()).ToString("f")});
                    }

                    if ((dr["Tax3Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RentTax3Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax3Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RentTax3Amount"].ToString()).ToString("f")});
                    }

                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Rent Deposit",
                                            GeneralFunctions.fnDouble(dr["RentDeposit"].ToString()).ToString("f")});

                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Rent Deposit Returned",
                                            GeneralFunctions.fnDouble(dr["RentDepositReturned"].ToString()).ToString("f")});




                }


                if ((GeneralFunctions.fnDouble(dr["RepairSales"].ToString()) != 0) || (GeneralFunctions.fnDouble(dr["RepairDeposit"].ToString()) != 0))
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "REPAIR",
                                            ""});
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Repair Sales",
                                            GeneralFunctions.fnDouble(dr["RepairSales"].ToString()).ToString("f")});


                    if ((dr["Tax1Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RepairTax1Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax1Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RepairTax1Amount"].ToString()).ToString("f")});
                    }
                    if ((dr["Tax2Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RepairTax2Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax2Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RepairTax2Amount"].ToString()).ToString("f")});
                    }

                    if ((dr["Tax3Exist"].ToString() == "Y") && (GeneralFunctions.fnDouble(dr["RepairTax3Amount"].ToString()) != 0))
                    {
                        intExpID++;
                        dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr["Tax3Name"].ToString(),
                                            GeneralFunctions.fnDouble(dr["RepairTax3Amount"].ToString()).ToString("f")});
                    }


                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Repair Deposit",
                                            GeneralFunctions.fnDouble(dr["RepairDeposit"].ToString()).ToString("f")});

                }


                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Item Discount",
                                           (GeneralFunctions.fnDouble(dr["DiscountItemAmount"].ToString()) + GeneralFunctions.fnDouble(dr["SDiscountItemAmount"].ToString())
                                          + GeneralFunctions.fnDouble(dr["BDiscountItemAmount"].ToString()) + GeneralFunctions.fnDouble(dr["RDiscountItemAmount"].ToString())).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Item Disount - Product Sales",
                                            GeneralFunctions.fnDouble(dr["DiscountItemAmount"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Item Discount - Service Sales",
                                            GeneralFunctions.fnDouble(dr["SDiscountItemAmount"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Item Discount - Other Sales",
                                            GeneralFunctions.fnDouble(dr["BDiscountItemAmount"].ToString()).ToString("f")});

                if (GeneralFunctions.fnDouble(dr["RDiscountItemAmount"].ToString()) != 0)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Item Discount - Repair",
                                            GeneralFunctions.fnDouble(dr["RDiscountItemAmount"].ToString()).ToString("f")});
                }

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Invoice Discount",
                                           (GeneralFunctions.fnDouble(dr["DiscountInvoiceAmount"].ToString()) + GeneralFunctions.fnDouble(dr["RntDiscountInvoiceAmount"].ToString())
                                          + GeneralFunctions.fnDouble(dr["RDiscountInvoiceAmount"].ToString())).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Invoice Discount - Sales",
                                            GeneralFunctions.fnDouble(dr["DiscountInvoiceAmount"].ToString()).ToString("f")});

                if (GeneralFunctions.fnDouble(dr["RntDiscountInvoiceAmount"].ToString()) != 0)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Invoice Discount - Rent",
                                            GeneralFunctions.fnDouble(dr["RntDiscountInvoiceAmount"].ToString()).ToString("f")});
                }

                if (GeneralFunctions.fnDouble(dr["RDiscountInvoiceAmount"].ToString()) != 0)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Invoice Discount - Repair",
                                            GeneralFunctions.fnDouble(dr["RDiscountInvoiceAmount"].ToString()).ToString("f")});
                }


                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Fees & Charge",
                                           (GeneralFunctions.fnDouble(dr["SalesFees"].ToString()) + GeneralFunctions.fnDouble(dr["SalesFeesTax"].ToString())
                                          + GeneralFunctions.fnDouble(dr["RentFees"].ToString()) + GeneralFunctions.fnDouble(dr["RentFeesTax"].ToString())
                                          + GeneralFunctions.fnDouble(dr["RepairFees"].ToString()) + GeneralFunctions.fnDouble(dr["RepairFeesTax"].ToString())).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Fees - Sales",
                                           (GeneralFunctions.fnDouble(dr["SalesFees"].ToString()) + GeneralFunctions.fnDouble(dr["SalesFeesTax"].ToString())).ToString("f")});

                if (GeneralFunctions.fnDouble(dr["RentFees"].ToString()) + GeneralFunctions.fnDouble(dr["RentFeesTax"].ToString()) != 0)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Fees - Rent",
                                           (GeneralFunctions.fnDouble(dr["RentFees"].ToString()) + GeneralFunctions.fnDouble(dr["RentFeesTax"].ToString())).ToString("f")});
                }

                if (GeneralFunctions.fnDouble(dr["RepairFees"].ToString()) + GeneralFunctions.fnDouble(dr["RepairFeesTax"].ToString()) != 0)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Fees - Repair",
                                            (GeneralFunctions.fnDouble(dr["RepairFees"].ToString()) + GeneralFunctions.fnDouble(dr["RepairFeesTax"].ToString())).ToString("f")});
                }

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "TENDER",
                                            ""});
                int ECID = 0;
                ECID = GeneralFunctions.fnInt32(dr["CloseoutID"].ToString());
                PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
                objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtblT.Rows.Clear();
                dtblT = objCloseout.ShowTenderRecord("T", ECID, Settings.TerminalName);
                foreach (DataRow dr1 in dtblT.Rows)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr1["TenderName"].ToString(),
                                            GeneralFunctions.fnDouble(dr1["TenderAmount"].ToString()).ToString("f")});
                }

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "OTHER",
                                            ""});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Layaway Sales Posted",
                                            GeneralFunctions.fnDouble(dr["LayawaySalesPosted"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Gift Cert. Sold",
                                            GeneralFunctions.fnDouble(dr["GCsold"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Bottle Deposit",
                                            GeneralFunctions.fnDouble(dr["BottleRefund"].ToString()).ToString("f")});

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Cost Of Goods Sold",
                                            GeneralFunctions.fnDouble(dr["CostOfGoods"].ToString()).ToString("f")});


                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "DEPARTMENT SALES",
                                            ""});

                double tot = 0;

                PosDataObject.Closeout objCloseoutD = new PosDataObject.Closeout();
                objCloseoutD.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtblD.Rows.Clear();
                dtblD = objCloseoutD.ShowSalesByDept_QuickExport(ECID, Settings.TerminalName);
                foreach (DataRow dr2 in dtblD.Rows)
                {
                    intExpID++;
                    dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            dr2["DeptID"].ToString() + " - " + dr2["DeptDesc"].ToString(),
                                            GeneralFunctions.fnDouble(dr2["SalesAmount"].ToString()).ToString("f")});

                    tot = tot + GeneralFunctions.fnDouble(dr2["SalesAmount"].ToString());
                }

                intExpID++;
                dtbl.Rows.Add(new object[] {intExpID.ToString(),
                                            dr["CloseoutID"].ToString(),
                                            dr["StartDateTime"].ToString(),
                                            dr["EndDateTime"].ToString(),
                                            "Total Department Sales",
                                            tot.ToString("f")});


            }

            DataTable dCount = dtbl.DefaultView.ToTable(true, "CO_ID");
            CLSC = dCount.Rows.Count;

            if (dCount.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dCount.Rows)
                {
                    if (i == 1)
                    {
                        StreamWriter writer = new StreamWriter(mem_location.Text.Trim());
                        StringBuilder builder = new StringBuilder();
                        try
                        {
                            int prevlength = 0;
                            string sepChar = GetSeparator();

                            string sep = "";

                            if (chkColumn.IsChecked == true)
                            {
                                foreach (DataColumn dc in dtbl.Columns)
                                {
                                    builder.Append(sep).Append(dc.ColumnName);
                                    sep = sepChar;
                                }
                                writer.WriteLine(builder.ToString());
                                prevlength = builder.Length;
                            }


                            foreach (DataRow drE in dtbl.Rows)
                            {
                                if (dr["CO_ID"].ToString() != drE["CO_ID"].ToString()) continue;
                                sep = "";
                                //builder.Remove(0, builder.Length);
                                foreach (DataColumn dc1 in dtbl.Columns)
                                {
                                    builder.Append(sep).Append(drE[dc1.ColumnName]);
                                    sep = sepChar;

                                }
                                writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                                prevlength = builder.Length;

                            }
                            writer.Close();

                        }
                        catch
                        {
                            rtn = 2;
                        }
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(mem_location.Text.Trim());
                        string tmp = fi.Extension;
                        string tmp1 = mem_location.Text.Trim().Replace(tmp, "");
                        tmp1 = tmp1 + "_" + dr["CO_ID"].ToString() + tmp;

                        StreamWriter writer = new StreamWriter(tmp1);
                        StringBuilder builder = new StringBuilder();
                        try
                        {
                            int prevlength = 0;
                            string sepChar = GetSeparator();

                            string sep = "";

                            if (chkColumn.IsChecked == true)
                            {
                                foreach (DataColumn dc in dtbl.Columns)
                                {
                                    builder.Append(sep).Append(dc.ColumnName);
                                    sep = sepChar;
                                }
                                writer.WriteLine(builder.ToString());
                                prevlength = builder.Length;
                            }


                            foreach (DataRow drE in dtbl.Rows)
                            {
                                if (dr["CO_ID"].ToString() != drE["CO_ID"].ToString()) continue;
                                sep = "";
                                //builder.Remove(0, builder.Length);
                                foreach (DataColumn dc1 in dtbl.Columns)
                                {
                                    builder.Append(sep).Append(drE[dc1.ColumnName]);
                                    sep = sepChar;

                                }
                                writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                                prevlength = builder.Length;

                            }
                            writer.Close();

                        }
                        catch
                        {
                            rtn = 2;
                        }
                    }

                    i++;
                }
            }
            else
            {
                rtn = 1;
            }
            return rtn;
        }

        private int ExecutePayrollExport()
        {
            int rtn = 0;
            DataTable dtbl = new DataTable();
            DataTable dtblH = new DataTable();
            DataTable dtblT = new DataTable();
            int intExpID = 0;
            PosDataObject.Attendance objCloseoutM = new PosDataObject.Attendance();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblH = objCloseoutM.FetchAttendanceData(GeneralFunctions.fnInt32(cmbEmployee.EditValue), dtFrom.DateTime, dtTo.DateTime);

            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("FIRSTNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("LASTNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("STARTDATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ENDDATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("STARTTIME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ENDTIME", System.Type.GetType("System.String"));
            //dtbl.Columns.Add("Shift", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtblH.Rows)
            {
                dtbl.Rows.Add(new object[] {
                                            dr["EMPCODE"].ToString(),
                                            dr["FNAME"].ToString(),
                                            dr["LNAME"].ToString(),
                                            dr["EMPRATE"].ToString(),
                                            dr["STARTDATE"].ToString(),
                                            dr["ENDDATE"].ToString(),
                                            dr["STARTTIME"].ToString(),
                                            dr["ENDTIME"].ToString()});
                //dr["Shift"].ToString()});
            }

            if (dtbl.Rows.Count > 0)
            {
                StreamWriter writer = new StreamWriter(mem_location.Text.Trim());
                StringBuilder builder = new StringBuilder();
                try
                {
                    int prevlength = 0;
                    string sepChar = GetSeparator();

                    string sep = "";

                    if (chkColumn.IsChecked == true)
                    {

                        foreach (DataColumn dc in dtbl.Columns)
                        {
                            builder.Append(sep).Append(dc.ColumnName);
                            sep = sepChar;
                        }
                        writer.WriteLine(builder.ToString());
                        prevlength = builder.Length;
                    }

                    foreach (DataRow drE in dtbl.Rows)
                    {
                        sep = "";
                        //builder.Remove(0, builder.Length);
                        foreach (DataColumn dc1 in dtbl.Columns)
                        {
                            builder.Append(sep).Append(drE[dc1.ColumnName]);
                            sep = sepChar;

                        }
                        writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                        prevlength = builder.Length;

                    }
                    writer.Close();

                }
                catch
                {
                    rtn = 2;
                }
            }
            else
            {
                rtn = 1;
            }
            return rtn;
        }

        private bool ValidAll()
        {
            if (mem_location.Text.Trim().Length == 0)
            {
                DocMessage.MsgEnter(Properties.Resources.Export_directory_and_filename);
                GeneralFunctions.SetFocus(btnBrowse);
                return false;
            }
            if (mem_location.Text.Trim().Length > 0)
            {
                if (!Directory.GetParent(mem_location.Text.Trim()).Exists)
                {
                    DocMessage.MsgInformation(Properties.Resources.Invalid_Export_Path);
                    GeneralFunctions.SetFocus(mem_location);
                    return false;
                }
            }

            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Date);
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }

            if (rbCustom.IsChecked == true)
            {
                if (txtChar.Text.Length == 0)
                {
                    DocMessage.MsgEnter(Properties.Resources.Custom_Separator);
                    GeneralFunctions.SetFocus(txtChar);
                    return false;
                }
            }

            return true;
        }

        private void CmbExport_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
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
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
