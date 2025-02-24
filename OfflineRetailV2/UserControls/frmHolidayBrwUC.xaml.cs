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
    /// Interaction logic for frmHolidayBrwUC.xaml
    /// </summary>
    public partial class frmHolidayBrwUC : UserControl
    {
        public frmHolidayBrwUC()
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
        private int intYear;
        public int Year
        {
            get { return intYear; }
            set { intYear = value; }
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
            for (intColCtr = 0; intColCtr < (grdHoliday.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdHoliday, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdHoliday.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdHoliday, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdHoliday.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdHoliday, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdHoliday.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void PopulateYear()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Year");
            for (int intCount = intYear - 5; intCount <= intYear + 5; intCount++)
            {
                dtbl.Rows.Add(new object[] { intCount.ToString() });
            }

            cmbYear.ItemsSource = dtbl;
            dtbl.Dispose();
            cmbYear.EditValue = intYear.ToString();
        }

        public void FetchGridData()
        {
            PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
            objHolidays.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objHolidays.FetchData(intYear);

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



            grdHoliday.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();


            
        }

        private void CmbYear_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            intYear = GeneralFunctions.fnInt32(cmbYear.EditValue.ToString());
            FetchGridData();
        }

        private async void BarbtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int intMaxID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmHolidayDlg frmHolidayDlg = new frmHolidayDlg();
            try
            {
                frmHolidayDlg.ID = 0;
                frmHolidayDlg.AddEdit = "Add";
                frmHolidayDlg.BrowseForm = this;
                frmHolidayDlg.ShowDialog();
                intMaxID = frmHolidayDlg.NewID;
            }
            finally
            {
                frmHolidayDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intMaxID > 0) await SetCurrentRow(intMaxID);
        }

        private async Task EditProcess()
        {
            int NewRecdID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdHoliday.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frmHolidayDlg frmHolidayDlg = new frmHolidayDlg();
            try
            {
                frmHolidayDlg.ID = await ReturnRowID();
                if (frmHolidayDlg.ID > 0)
                {
                    frmHolidayDlg.AddEdit = "Edit";
                    frmHolidayDlg.BrowseForm = this;
                    frmHolidayDlg.ShowDialog();
                    NewRecdID = frmHolidayDlg.NewID;
                }
            }
            finally
            {
                frmHolidayDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            if (NewRecdID > 0) await SetCurrentRow(NewRecdID);
        }

        private async void BarbtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void BarbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((grdHoliday.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRecdID = 0;
            intRecdID = gridView1.FocusedRowHandle;
            if (intRecdID == -1) return;

            intRecdID = await ReturnRowID();

            SetRowIndex();

            if (DocMessage.MsgDelete(Properties.Resources.Holiday))
            {
                PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
                objHolidays.Connection = SystemVariables.Conn;
                string strdelete = objHolidays.DeleteRecord(intRecdID);
                FetchGridData();
                if ((grdHoliday.ItemsSource as DataTable).Rows.Count > 0)
                {
                    if (intRecdID != 0)
                        intSelectedRowHandle = intRecdID - 1;
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void BarButtonItem1_Click(object sender, RoutedEventArgs e)
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
                dtbl = grdHoliday.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep2CListF rep_list = new OfflineRetailV2.Report.Misc.rep2CListF();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "List of Holidays";
                rep_list.rFilter.Text = "Year - " + intYear.ToString();
                rep_list.rHcol1.Text = "Date";
                rep_list.rHcol2.Text = "Occasion";

                rep_list.rCol1.DataBindings.Add("Text", dtbl, "HolidayDate");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "HolidayDesc");

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

        private void CmbYear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Holidays")
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
                txtSearchGrdData.InfoText = "Search Holidays";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Holidays") return;
            if (txtSearchGrdData.Text == "")
            {
                grdHoliday.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdHoliday.FilterString = "([HolidayDesc] LIKE '%" + filterValue + "%')";
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
