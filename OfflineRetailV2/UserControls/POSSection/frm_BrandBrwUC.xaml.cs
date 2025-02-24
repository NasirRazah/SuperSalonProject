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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_BrandBrwUC.xaml
    /// </summary>
    public partial class frm_BrandBrwUC : UserControl
    {
        public frm_BrandBrwUC()
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
            fParent.RedirectToSubmenu("Items");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdBrand.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdBrand, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdBrand.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdBrand, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdBrand.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdBrand, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdBrand.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Brand obj = new PosDataObject.Brand();
            obj.Connection = SystemVariables.Conn;
            dbtbl = obj.FetchData();

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



            grdBrand.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssBrand) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BrandDlg frm_Brand = new frm_BrandDlg();
            try
            {
                frm_Brand.ID = 0;
                frm_Brand.BrowseForm = this;
                frm_Brand.ShowDialog();
                intNewRecID = frm_Brand.NewID;
            }
            finally
            {
                
                frm_Brand.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdBrand.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BrandDlg frm_Brand = new frm_BrandDlg();
            try
            {
                frm_Brand.ID = await ReturnRowID();
                if (frm_Brand.ID > 0)
                {
                    frm_Brand.ShowData();
                    frm_Brand.BrowseForm = this;
                    frm_Brand.ShowDialog();
                    intNewRecID = frm_Brand.ID;
                }
            }
            finally
            {
                frm_Brand.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssBrand) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            await EditProcess();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssDepartment) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdBrand.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Family))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("Product", "BrandID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_family_used_Product);
                        return;
                    }
                    PosDataObject.Brand objDepartment = new PosDataObject.Brand();
                    objDepartment.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objDepartment.DeleteRecord(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException(Properties.Resources.Deleting_Family, strError);
                    }
                    FetchData();
                    if ((grdBrand.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        gridView1.FocusedRowHandle = intRowID - 1;
                        //await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private async void GrdBrand_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssBrand) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            await EditProcess();
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
                dtbl = grdBrand.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep2CList rep_list = new OfflineRetailV2.Report.Misc.rep2CList();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "List of Item Families";

                rep_list.rHcol1.Text = "Family ID";
                rep_list.rHcol2.Text = "Family Name";

                rep_list.rCol1.DataBindings.Add("Text", dtbl, "BrandID");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "BrandDescription");

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
            if (txtSearchGrdData.InfoText == "Search Families")
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
                txtSearchGrdData.InfoText = "Search Families";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Families") return;
            if (txtSearchGrdData.Text == "")
            {
                grdBrand.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdBrand.FilterString = "([BrandDescription] LIKE '%" + filterValue + "%')";
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
