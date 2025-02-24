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
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frmShiftBrwUC.xaml
    /// </summary>
    public partial class frmShiftBrwUC : UserControl
    {
        public frmShiftBrwUC()
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

        private bool lbDeleteMode;
        private int intSelectedRowHandle;

        public int SelectedRowHandle
        {
            get { return intSelectedRowHandle; }
            set { intSelectedRowHandle = value; }
        }

        public bool DeleteMode
        {
            get { return lbDeleteMode; }
            set { lbDeleteMode = value; }
        }


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
            for (intColCtr = 0; intColCtr < (grdShift.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdShift, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdShift, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdShift, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        private void FocusGridRow()
        {
            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return;
            if (lbDeleteMode)
            {
                if (intSelectedRowHandle != 0)
                    gridView1.FocusedRowHandle = intSelectedRowHandle - 1;
            }
            else
            {
                if (intSelectedRowHandle == -1)
                {
                    gridView1.FocusedRowHandle = (grdShift.ItemsSource as DataTable).Rows.Count - 1;
                }
                else
                {
                    gridView1.FocusedRowHandle = intSelectedRowHandle;
                }
            }
            intSelectedRowHandle = 0;
        }

        public void FetchGridData()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objShift.FetchData();

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



            grdShift.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmShiftDlg frmShiftDlg = new frmShiftDlg();
            int NewRecDId = 0;
            try
            {
                frmShiftDlg.ID = 0;
                frmShiftDlg.AddEdit = "New";
                frmShiftDlg.BrowseForm = this;
                frmShiftDlg.ShowDialog();
                NewRecDId = frmShiftDlg.NewID;
            }
            finally
            {
                frmShiftDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (NewRecDId > 0) await SetCurrentRow(NewRecDId);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmShiftDlg frmShiftDlg = new frmShiftDlg();
            try
            {
                frmShiftDlg.ID = await ReturnRowID();
                
                PosDataObject.Shift objShift = new PosDataObject.Shift();
                objShift.Connection = SystemVariables.Conn;
                if (objShift.IfExistShift(frmShiftDlg.ID) == 0)
                {
                    if (frmShiftDlg.ID > 0)
                    {
                        frmShiftDlg.AddEdit = "Edit";
                        frmShiftDlg.BrowseForm = this;
                        frmShiftDlg.ShowDialog();
                        intNewRecID = frmShiftDlg.NewID;
                    }
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources. Cannot_edit_the_record_as_there_exists_assigned_employees_for_this_Shift);
                }
            }
            finally
            {
                frmShiftDlg.Close();
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

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((grdShift.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRecdID = 0;
            intRecdID = gridView1.FocusedRowHandle;
            if (intRecdID == -1) return;

            intRecdID = await ReturnRowID();

            SetRowIndex();

            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            if (objShift.IfExistShift(intRecdID) == 0)
            {
                if (DocMessage.MsgDelete("Shift"))
                {

                    PosDataObject.Shift objShift1 = new PosDataObject.Shift();
                    objShift1.Connection = SystemVariables.Conn;
                    string strdelete = objShift1.DeleteRecord(intRecdID);
                    FetchGridData();
                    if ((grdShift.ItemsSource as DataTable).Rows.Count > 0)
                    {
                        if (intRecdID != 0)
                            intSelectedRowHandle = intRecdID - 1;
                    }
                }
            }
            else
            {
                DocMessage.MsgInformation(Properties.Resources.Cannot_Delete_the_record_as_there_exists_Assigned_Tasks_for_this_Shift);
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                DataTable dtbl = new DataTable();
                dtbl = grdShift.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep5CList1 rep_list = new OfflineRetailV2.Report.Misc.rep5CList1();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "List of Shifts";


                rep_list.rCol1.DataBindings.Add("Text", dtbl, "ShiftName");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "StartTime");
                rep_list.rCol3.DataBindings.Add("Text", dtbl, "EndTime");
                rep_list.rCol4.DataBindings.Add("Text", dtbl, "ShiftDuration");
                rep_list.rCol5.DataBindings.Add("Text", dtbl, "ShiftStatus");

                if (Settings.ReportPrinterName != "") rep_list.PrinterName = Settings.ReportPrinterName;
                rep_list.CreateDocument();
                rep_list.PrintingSystem.ShowMarginsWarning = false;

                //rep_list.ShowPreviewDialog();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                window.PreviewControl.DocumentSource = rep_list;
                window.ShowDialog();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                rep_list.Dispose();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Shifts")
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
                txtSearchGrdData.InfoText = "Search Shifts";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();

        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Shifts") return;
            if (txtSearchGrdData.Text == "")
            {
                grdShift.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdShift.FilterString = "([ShiftName] LIKE '%" + filterValue + "%')";
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
