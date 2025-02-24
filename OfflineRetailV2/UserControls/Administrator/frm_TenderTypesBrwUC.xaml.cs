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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_TenderTypesBrwUC.xaml
    /// </summary>
    public partial class frm_TenderTypesBrwUC : UserControl
    {
        public frm_TenderTypesBrwUC()
        {
            InitializeComponent();
            btnup.Click += Btnup_Click;
            btndown.Click += Btndown_Click;
            grdTenderTypes.CurrentItemChanged += GrdTenderTypes_CurrentItemChanged;
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

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Administrator");
        }

        private void GrdTenderTypes_CurrentItemChanged(object sender, DevExpress.Xpf.Grid.CurrentItemChangedEventArgs e)
        {
            EnableDisableButton();
        }

        private int intSelectedRowHandle;

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdTenderTypes.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdTenderTypes, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTenderTypes, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTenderTypes, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {

            DataTable dbtbl = new DataTable();
            PosDataObject.TenderTypes objDTenderTypes = new PosDataObject.TenderTypes();
            objDTenderTypes.Connection = SystemVariables.Conn;
            dbtbl = objDTenderTypes.FetchData();


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



            grdTenderTypes.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        public void EnableDisableButton()
        {

            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0)
            {
                btnup.IsEnabled = false;
                btndown.IsEnabled = false;
            }
            else if (gridView1.FocusedRowHandle == 0)
            {
                btnup.IsEnabled = false;
                btndown.IsEnabled = true;
            }
            else if (gridView1.FocusedRowHandle == (grdTenderTypes.ItemsSource as DataTable).Rows.Count - 1)
            {
                btnup.IsEnabled = true;
                btndown.IsEnabled = false;
            }
            else
            {
                btnup.IsEnabled = true;
                btndown.IsEnabled = true;
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            //EnableDisableButton();
        }

        private async void BarButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_TenderTypesDlg frm_TenderTypesDlg = new frm_TenderTypesDlg();
            try
            {
                frm_TenderTypesDlg.ID = 0;
                frm_TenderTypesDlg.BrowseForm = this;
                frm_TenderTypesDlg.ShowDialog();
                intNewRecID = frm_TenderTypesDlg.NewID;
            }
            finally
            {
                frm_TenderTypesDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                await SetCurrentRow(intNewRecID);
                EnableDisableButton();
            }

            
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_TenderTypesDlg frm_TenderTypesDlg = new frm_TenderTypesDlg();
            try
            {
                frm_TenderTypesDlg.ID = await ReturnRowID();
                if (frm_TenderTypesDlg.ID > 0)
                {
                    frm_TenderTypesDlg.BrowseForm = this;
                    frm_TenderTypesDlg.ShowDialog();
                    intNewRecID = frm_TenderTypesDlg.ID;
                }
            }
            finally
            {
                frm_TenderTypesDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
            EnableDisableButton();
        }

        private async void BarButtonItem2_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void BarButtonItem3_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.tender_types))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("Tender", "TenderType", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_tender_type_as_it_is_already_used);
                        return;
                    }
                    PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
                    objTenderTypes.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intOrder = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder));
                    int ret = UpdateOrder(intRecdID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder)),
                     "D");
                    if (ret != 0)
                    {
                        //DocMessage.ShowException("Deleting Tender Type", strError);
                    }
                    FetchData();
                    if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                    EnableDisableButton();
                }
            }
        }

        private int UpdateOrder(int pID, int pOrder, string pType)
        {
            PosDataObject.TenderTypes objCategory = new PosDataObject.TenderTypes();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.UpdatePaymentOrder(pID, pOrder, pType);
        }

        private async void Btnup_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder)), "P");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();
        }

        private async void Btndown_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder)),
                "N");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();
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
                dtbl = grdTenderTypes.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep4CList2 rep_list = new OfflineRetailV2.Report.Misc.rep4CList2();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "List of Tender Types";

                rep_list.rHcol1.Text = "Tender Type";
                rep_list.rHcol2.Text = "Display As";
                rep_list.rHcol3.Text = "Enabled?";
                rep_list.rHcol4.Text = "Open Cash Drawer?";

                rep_list.rCol1.DataBindings.Add("Text", dtbl, "Name");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "DisplayAs");
                rep_list.rCol3.DataBindings.Add("Text", dtbl, "Enabled");
                rep_list.rCol4.DataBindings.Add("Text", dtbl, "IsOpenCashDrawer");

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
            if (txtSearchGrdData.InfoText == "Search Tender Types")
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
                txtSearchGrdData.InfoText = "Search Tender Types";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Tender Types") return;
            if (txtSearchGrdData.Text == "")
            {
                grdTenderTypes.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdTenderTypes.FilterString = "([Name] LIKE '%" + filterValue + "%' OR [DisplayAs] LIKE '%" + filterValue + "%')";
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
