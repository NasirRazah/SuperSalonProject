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
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_EmployeeBrwUC.xaml
    /// </summary>
    public partial class frm_EmployeeBrwUC : UserControl
    {
        public frm_EmployeeBrwUC()
        {
            InitializeComponent();
        }

        private FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool bOpenKeyBrd = false;




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

        public void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private int intSelectedRowHandle;

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Employee");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdEmployee.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdEmployee, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            dbtbl = objEmployee.FetchData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }



            grdEmployee.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssEmployeeAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_EmployeeDlg frm_EmployeeDlg = new frm_EmployeeDlg();
            try
            {
                frm_EmployeeDlg.ID = 0;
                frm_EmployeeDlg.BrowseForm = this;
                frm_EmployeeDlg.ShowDialog();
                intNewRecID = frm_EmployeeDlg.NewID;
            }
            finally
            {
                frm_EmployeeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssEmployeeEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_EmployeeDlg frm_EmployeeDlg = new frm_EmployeeDlg();
            try
            {
                frm_EmployeeDlg.ID = await ReturnRowID();
                if (frm_EmployeeDlg.ID > 0)
                {
                    frm_EmployeeDlg.BrowseForm = this;
                    frm_EmployeeDlg.ShowDialog();
                    intNewRecID = frm_EmployeeDlg.ID;
                }
            }
            finally
            {
                frm_EmployeeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private DataTable GetDeletedocsNphotosID(int refID)
        {
            DataTable db = new DataTable();
            db.Columns.Add("ID", System.Type.GetType("System.String"));
            string fileext = "";
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objNotes.FetchNoteData("", "Employee", refID, SystemVariables.DateFormat);
            foreach (DataRow dr in dbtbl.Rows)
            {
                if (dr["Attach"].ToString() == "Y") db.Rows.Add(new object[] { dr["ID"].ToString() });
            }
            return db;
        }

        private void DeletedocsNphotos(DataTable reftable)
        {

            string sdoc = "";
            string sscan = "";

            string csStorePath = "";
            csStorePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Employee\\Notes\\";
            else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Employee\\Notes\\";



            DirectoryInfo di = new DirectoryInfo(csStorePath);
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi)
            {
                foreach (DataRow dr in reftable.Rows)
                {
                    if ((f.Name.Contains(dr["ID"].ToString() + "_d")) || (f.Name.Contains(dr["ID"].ToString() + "_s")))
                    {
                        f.Delete();
                    }
                }
            }

        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssEmployeeDelete) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0)
            {
                // DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please Select an Customer");
                return;
            }
            //SetRowIndex();

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (intRecdID == SystemVariables.CurrentUserID)
                {
                    DocMessage.MsgInformation(Properties.Resources.Logged_on_employee_cannot_be_deleted);
                    if (Settings.ActiveAdminPswdForMercury)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                        else
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                    }
                    return;
                }
                if (DocMessage.MsgDelete(Properties.Resources.Employee))
                {
                    DataTable dtbldocphoto = new DataTable();
                    dtbldocphoto = GetDeletedocsNphotosID(intRecdID);
                    PosDataObject.Employee objEmployee = new PosDataObject.Employee();
                    objEmployee.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intReturn = objEmployee.DeleteRecord(intRecdID);
                    if (intReturn != 0)
                    {
                        if (Settings.ActiveAdminPswdForMercury)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                        }

                        if (Settings.ActiveAdminPswdForMercury)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event6, "Failed", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                        }

                        if (intReturn == 1)
                            DocMessage.MsgInformation(Properties.Resources.Cannot_delete_this_employee_as_there_exists_ + " " + Properties.Resources.attendance_informations_against_it);
                        if (intReturn == 2)
                            DocMessage.MsgInformation(Properties.Resources.Cannot_delete_this_employee_as_there_exists_ + " " + Properties.Resources.mails_against_it);
                        if (intReturn == 3)
                            DocMessage.MsgInformation(Properties.Resources.Cannot_delete_this_employee_as_there_exists_ + " " + Properties.Resources.appointments_against_it);
                        if (intReturn == 4)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_record_as_it_is_already_exported);
                        return;
                    }
                    else
                    {
                        if (Settings.ActiveAdminPswdForMercury)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event6, "Successful", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event6, "Successful", await GeneralFunctions.GetCellValue1(intRowID, grdEmployee, colEmpID));
                        }
                    }
                    if (dtbldocphoto.Rows.Count > 0)
                    {
                        try
                        {
                            DeletedocsNphotos(dtbldocphoto);
                        }
                        catch
                        {
                        }
                    }
                    FetchData();
                    if ((grdEmployee.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssEmployeePrint) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                return;
            }
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.Employee objEmp = new PosDataObject.Employee();
                objEmp.Connection = SystemVariables.Conn;
                dtbl = objEmp.FetchRecordForReport(await ReturnRowID());
                OfflineRetailV2.Report.Employee.repEmployeeSnap rep_EmployeeSnap = new OfflineRetailV2.Report.Employee.repEmployeeSnap();

                rep_EmployeeSnap.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_EmployeeSnap);
                rep_EmployeeSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_EmployeeSnap.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_EmployeeSnap.rPic.DataBindings.Add("Text", dtbl, "ID");

                rep_EmployeeSnap.rID.DataBindings.Add("Text", dtbl, "EmployeeID");
                rep_EmployeeSnap.rName.DataBindings.Add("Text", dtbl, "Name");
                rep_EmployeeSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_EmployeeSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_EmployeeSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_EmployeeSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");
                rep_EmployeeSnap.rBstate.DataBindings.Add("Text", dtbl, "State");

                rep_EmployeeSnap.rphone1.DataBindings.Add("Text", dtbl, "Phone1");
                rep_EmployeeSnap.rphone2.DataBindings.Add("Text", dtbl, "Phone2");
                rep_EmployeeSnap.rEphone.DataBindings.Add("Text", dtbl, "EmergencyPhone");
                rep_EmployeeSnap.rEcontact.DataBindings.Add("Text", dtbl, "EmergencyContact");
                rep_EmployeeSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");

                rep_EmployeeSnap.rSecurityNo.DataBindings.Add("Text", dtbl, "SSNumber");
                rep_EmployeeSnap.rShift.DataBindings.Add("Text", dtbl, "Shift");
                rep_EmployeeSnap.rProfile.DataBindings.Add("Text", dtbl, "SecurityProfile");


                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                try
                {
                    if (Settings.ReportPrinterName != "") rep_EmployeeSnap.PrinterName = Settings.ReportPrinterName;
                    rep_EmployeeSnap.CreateDocument();
                    rep_EmployeeSnap.PrintingSystem.ShowMarginsWarning = false;
                    rep_EmployeeSnap.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_EmployeeSnap.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_EmployeeSnap;
                    window.ShowDialog();

                }
                finally
                {
                    rep_EmployeeSnap.Dispose();


                    dtbl.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Employees")
            {
                txtSearchGrdData.Text = "";
            }
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            if (bOpenKeyBrd) return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void TxtSearchGrdData_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.Text == "")
            {
                txtSearchGrdData.InfoText = "Search Employees";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Employees") return;
            if (txtSearchGrdData.Text == "")
            {
                grdEmployee.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdEmployee.FilterString = "([EmployeeID] LIKE '%" + filterValue + "%' OR [FirstName] LIKE '%" + filterValue + "%' OR [LastName] LIKE '%" + filterValue + "%' OR [Phone1] LIKE '%" + filterValue + "%' OR [EmergencyPhone] LIKE '%" + filterValue + "%' OR [EmergencyContact] LIKE '%" + filterValue + "%')";
            }
        }

        private void TxtSearchGrdData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutFullKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                bOpenKeyBrd = true;
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }
    }
}
