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
    /// Interaction logic for frm_CategoryBrwUC.xaml
    /// </summary>
    public partial class frm_CategoryBrwUC : UserControl
    {
        public frm_CategoryBrwUC()
        {
            InitializeComponent();
            btnUP.Click += BtnUP_Click;
            btnDown.Click += BtnDown_Click;
            grdCategory.CurrentItemChanged += GrdCategory_CurrentItemChanged;
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
            fParent.RedirectToSubmenu("Items");
        }

        private void GrdCategory_CurrentItemChanged(object sender, DevExpress.Xpf.Grid.CurrentItemChangedEventArgs e)
        {
            EnableDisableButton();
        }

        private int intSelectedRowHandle;

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdCategory.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdCategory, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task SetCurrentRowAfterDelete(int OrderID)
        {
            int intRecID = 0;
            int intOrderID = 0;
            int intColCtr = 0;

            for (intColCtr = 0; intColCtr < (grdCategory.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intOrderID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intColCtr, grdCategory, colDisplay));
                intRecID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intColCtr, grdCategory, colID));
                if (intOrderID == OrderID - 1) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdCategory, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdCategory, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }



        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            dbtbl = objCategory.FetchData();

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



            grdCategory.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();


        }

        private async void BarButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_CategoryDlg frm_CategoryDlg = new frm_CategoryDlg();
            try
            {
                frm_CategoryDlg.ID = 0;
                frm_CategoryDlg.BrowseForm = this;
                frm_CategoryDlg.ShowDialog();
                intNewRecID = frm_CategoryDlg.NewID;
            }
            finally
            {
                frm_CategoryDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                await SetCurrentRow(intNewRecID);
                EnableDisableButton();
            }
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_CategoryDlg frm_CategoryDlg = new frm_CategoryDlg();
            try
            {
                frm_CategoryDlg.ID = 0;
                frm_CategoryDlg.BrowseForm = this;
                frm_CategoryDlg.ShowDialog();
                intNewRecID = frm_CategoryDlg.NewID;
            }
            finally
            {
               
                frm_CategoryDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                await SetCurrentRow(intNewRecID);
                //EnableDisableButton();
            }
        }

        public void EnableDisableButton()
        {
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0)
            {
                btnUP.IsEnabled = false;
                btnDown.IsEnabled = false;
            }
            else if (gridView1.FocusedRowHandle == 0)
            {
                btnUP.IsEnabled = false;
                btnDown.IsEnabled = true;
            }
            else if (gridView1.FocusedRowHandle == (grdCategory.ItemsSource as DataTable).Rows.Count - 1)
            {
                btnUP.IsEnabled = true;
                btnDown.IsEnabled = false;
            }
            else
            {
                btnUP.IsEnabled = true;
                btnDown.IsEnabled = true;
            }
        }

        private int UpdateDisplayOrder(int pID, int pOrder, string pType)
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.UpdatePosDisplayOrder(pID, pOrder, pType);
        }

        private async void BtnUP_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateDisplayOrder(intRowID,
                GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdCategory, colDisplay)),
                "P");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();
        }

        private async void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateDisplayOrder(intRowID,
                GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdCategory, colDisplay)),
                "N");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            //EnableDisableButton();
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_CategoryDlg frm_CategoryDlg = new frm_CategoryDlg();
            try
            {
                frm_CategoryDlg.ID = await ReturnRowID();
                if (frm_CategoryDlg.ID > 0)
                {
                    //frm_CategoryDlg.ShowData();
                    frm_CategoryDlg.BrowseForm = this;
                    frm_CategoryDlg.ShowDialog();
                    intNewRecID = frm_CategoryDlg.ID;
                }
            }
            finally
            {
                frm_CategoryDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            await EditProcess();
        }

        private async void GrdCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            await EditProcess();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0)
            {
                // DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please Select an Customer");
                return;
            }
            //SetRowIndex();

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.POS_Screen_Category))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("Product", "CategoryID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_category_used_Product);
                        return;
                    }
                    PosDataObject.Category objCategory = new PosDataObject.Category();
                    objCategory.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intOrder = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdCategory, colDisplay));
                    int ret = UpdateDisplayOrder(intRecdID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdCategory, colDisplay)),
                    "D");
                    if (ret != 0)
                    {
                        //DocMessage.ShowException("Deleting Category", strError);
                    }
                    FetchData();
                    if ((grdCategory.ItemsSource as DataTable).Rows.Count >= 1)
                    {
                        await SetCurrentRowAfterDelete(intOrder);
                    }
                    EnableDisableButton();
                }
            }
        }

        private async void BtnBatchPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                PosDataObject.Product ocat1 = new PosDataObject.Product();
                ocat1.Connection = SystemVariables.Conn;
                int rcnt = ocat1.GetActiveProductsforCategory(intRecdID);

                if (rcnt == 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.No_active_product_against_category);
                    return;
                }
                else
                {
                    if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_transfer + " " + rcnt.ToString() + Properties.Resources.product_for_batch_print) == MessageBoxResult.Yes)
                    {
                        PosDataObject.Product ocat2 = new PosDataObject.Product();
                        ocat2.Connection = SystemVariables.Conn;
                        ocat2.LoginUserID = SystemVariables.CurrentUserID;
                        bool b = ocat2.TransferToPrintLabel(intRecdID);
                        if (b)
                        {
                            DocMessage.MsgInformation(Properties.Resources.Succesfully_Transferred);
                            return;
                        }
                        else
                        {
                            DocMessage.MsgInformation(Properties.Resources.Error);
                            return;
                        }
                    }
                }

            }
        }

        private int ItemCount(int prm)
        {
            PosDataObject.Product objgrp = new PosDataObject.Product();
            objgrp.Connection = SystemVariables.Conn;
            return objgrp.GetItemCountInGroup(prm);
        }

        private async void BtnReorderItem_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdCategory.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (ItemCount(intRecdID) > 0)
                {
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                    frm_ItemOrderBrw frm_ItemOrderBrw = new frm_ItemOrderBrw();
                    try
                    {

                        frm_ItemOrderBrw.frm_ItemOrderBrwUC.lbCategory.Text = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdCategory, colDescription);
                        frm_ItemOrderBrw.GroupID = intRecdID;
                        frm_ItemOrderBrw.ShowDialog();
                    }
                    finally
                    {
                        frm_ItemOrderBrw.Close();
                        (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
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
                dtbl = grdCategory.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep5CList rep_list = new OfflineRetailV2.Report.Misc.rep5CList();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "POS Screen Categories";

                rep_list.rHcol1.Text = "Cat. ID";
                rep_list.rHcol2.Text = "Category";
                rep_list.rHcol3.Text = "POS Display Order";
                rep_list.rHcol4.Text = "Max Items for POS";
                rep_list.rHcol5.Text = "POS Display?";

                rep_list.rCol1.DataBindings.Add("Text", dtbl, "CategoryID");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "Description");
                rep_list.rCol3.DataBindings.Add("Text", dtbl, "PosDisplayOrder");
                rep_list.rCol4.DataBindings.Add("Text", dtbl, "MaxItemsForPOS");
                rep_list.rCol5.DataBindings.Add("Text", dtbl, "AddToPOSScreen");
                rep_list.rHcol3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                rep_list.rHcol4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                rep_list.rHcol5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                rep_list.rCol3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                rep_list.rCol4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                rep_list.rCol5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

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
            if (txtSearchGrdData.InfoText == "Search Categories")
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
                txtSearchGrdData.InfoText = "Search Categories";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Categories") return;
            if (txtSearchGrdData.Text == "")
            {
                grdCategory.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdCategory.FilterString = "([Description] LIKE '%" + filterValue + "%')";
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
