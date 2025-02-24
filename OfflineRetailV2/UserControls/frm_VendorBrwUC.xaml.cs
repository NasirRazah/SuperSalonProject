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
    /// Interaction logic for frm_VendorBrwUC.xaml
    /// </summary>
    public partial class frm_VendorBrwUC : UserControl
    {
        public frm_VendorBrwUC()
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
            fParent.RedirectToSubmenu("Ordering");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdVendor.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdVendor, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdVendor, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdVendor, colID)));
            return intRecID;
        }

        public async Task<string> ReturnVendorName()
        {
            int intRowID = -1;
            string intRecID = "";
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return "";
            if (gridView1.FocusedRowHandle < 0) return "";
            intRowID = gridView1.FocusedRowHandle;
            intRecID = (await GeneralFunctions.GetCellValue1(intRowID, grdVendor, colName));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Vendor obj = new PosDataObject.Vendor();
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



            grdVendor.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

           
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssVendorAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_VendorDlg frmDlg = new frm_VendorDlg();
            try
            {
                frmDlg.ID = 0;
                frmDlg.BrowseForm = this;
                frmDlg.ShowDialog();
                intNewRecID = frmDlg.NewID;
            }
            finally
            {
                
                frmDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssVendorEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return;

            string qbtax = await ReturnVendorName();
            if (qbtax == "QB-XEPOS Vendor") return;


            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_VendorDlg frmDlg = new frm_VendorDlg();
            try
            {
                frmDlg.ID = await ReturnRowID();
                if (frmDlg.ID > 0)
                {
                    frmDlg.BrowseForm = this;
                    frmDlg.ShowDialog();
                    intNewRecID = frmDlg.ID;
                }
            }
            finally
            {
                frmDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
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
            if ((!SecurityPermission.AcssVendorDelete) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdVendor.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                string qbtax = await ReturnVendorName();
                if (qbtax == "QB-XEPOS Vendor") return;

                if (DocMessage.MsgDelete(Properties.Resources.Vendor))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("POHeader", "VendorID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_vendor_as_it_is_used_in_ + " " + "purchase order");
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("RecvHeader", "VendorID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_vendor_as_it_is_used_in_ + " " + Properties.Resources.received_items);
                        return;
                    }

                    if (IfExistInProduct(intRecdID) == 0)
                    {
                        if (GeneralFunctions.GetRecordCountForDelete("VendPart", "VendorID", intRecdID) > 0)
                        {
                            MessageBoxResult DlgResult = new MessageBoxResult();
                            DlgResult = new MessageBoxWindow().Show(Properties.Resources.Records_in_the_vendor_part_number_will_be_deleted_Are_you_sure_to_continue, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question);

                            if (DlgResult == MessageBoxResult.Yes)
                            {
                                PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
                                objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                string strError = objVendor.DeleteRecord(intRecdID);
                                PosDataObject.Vendor objVendor1 = new PosDataObject.Vendor();
                                objVendor1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                string strError1 = objVendor1.DeletePartNumber(intRecdID);
                                if ((strError != "") || (strError1 != ""))
                                {
                                    DocMessage.ShowException("Deleting Vendor", strError);
                                }
                                else
                                {

                                }
                                FetchData();
                                if ((grdVendor.ItemsSource as DataTable).Rows.Count > 1)
                                {
                                    gridView1.FocusedRowHandle = intRowID - 1;
                                    //await SetCurrentRow(intRecdID - 1);
                                }
                            }

                        }
                        else
                        {
                            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
                            objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            string strError = objVendor.DeleteRecord(intRecdID);
                            if (strError != "")
                            {
                                DocMessage.ShowException("Deleting Vendor", strError);
                            }
                            FetchData();
                            if ((grdVendor.ItemsSource as DataTable).Rows.Count > 1)
                            {
                                gridView1.FocusedRowHandle = intRowID - 1;
                                //await SetCurrentRow(intRecdID - 1);
                            }
                        }
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.This_Vendor_already_exists_in_Product);
                    }

                }
            }
        }

        private int IfExistInProduct(int venID)
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objVendor.IsExistProduct(venID);
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssVendorPrint) && (SystemVariables.CurrentUserID > 0))
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
                DataTable dtbl1 = new DataTable();
                PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
                objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objVendor.ShowRecord(await ReturnRowID());
                OfflineRetailV2.Report.Vendor.repVendorSnapDetail rep_VendorSnapDetail = new OfflineRetailV2.Report.Vendor.repVendorSnapDetail();
                OfflineRetailV2.Report.Vendor.repVendorSnap rep_VendorSnap = new OfflineRetailV2.Report.Vendor.repVendorSnap();
                OfflineRetailV2.Report.Vendor.repVendorPart rep_VendorPart = new OfflineRetailV2.Report.Vendor.repVendorPart();

                rep_VendorSnapDetail.subrepGeneral.ReportSource = rep_VendorSnap;
                rep_VendorSnap.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_VendorSnapDetail);
                rep_VendorSnap.rReportHeader.Text = Settings.ReportHeader;
                rep_VendorSnap.rName.DataBindings.Add("Text", dtbl, "Name");

                rep_VendorSnap.rID.DataBindings.Add("Text", dtbl, "VendorID");
                rep_VendorSnap.rContact.DataBindings.Add("Text", dtbl, "Contact");
                rep_VendorSnap.rPhone.DataBindings.Add("Text", dtbl, "Phone");
                rep_VendorSnap.rFax.DataBindings.Add("Text", dtbl, "Fax");
                rep_VendorSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");
                rep_VendorSnap.rNotes.DataBindings.Add("Text", dtbl, "Notes");

                rep_VendorSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_VendorSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_VendorSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_VendorSnap.rBcountry.DataBindings.Add("Text", dtbl, "Country");
                rep_VendorSnap.rBstate.DataBindings.Add("Text", dtbl, "State");
                rep_VendorSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");

                PosDataObject.Vendor objVendor1 = new PosDataObject.Vendor();
                objVendor1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objVendor1.ShowPartNumber(await ReturnRowID());

                if (dtbl1.Rows.Count > 0)
                {
                    rep_VendorSnapDetail.subrepPart.ReportSource = rep_VendorPart;
                    rep_VendorPart.Report.DataSource = dtbl1;
                    rep_VendorPart.DecimalPlace = Settings.DecimalPlace;
                    rep_VendorPart.rProduct.DataBindings.Add("Text", dtbl1, "ProductName");
                    rep_VendorPart.rPart.DataBindings.Add("Text", dtbl1, "PartNumber");
                    rep_VendorPart.rCost.DataBindings.Add("Text", dtbl1, "Price");
                    rep_VendorPart.rPrimary.DataBindings.Add("Text", dtbl1, "Primary");
                }

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_VendorSnapDetail.PrinterName = Settings.ReportPrinterName;
                    rep_VendorSnapDetail.CreateDocument();
                    rep_VendorSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    rep_VendorSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_VendorSnapDetail.ShowPreviewDialog();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_VendorSnapDetail;
                    window.ShowDialog();
                    
                }
                finally
                {
                    rep_VendorSnap.Dispose();
                    rep_VendorPart.Dispose();
                    rep_VendorSnapDetail.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Vendors")
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
                txtSearchGrdData.InfoText = "Search Vendors";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Vendors") return;
            if (txtSearchGrdData.Text == "")
            {
                grdVendor.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdVendor.FilterString = "([Name] LIKE '%" + filterValue + "%' OR [Contact] LIKE '%" + filterValue + "%')";
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
