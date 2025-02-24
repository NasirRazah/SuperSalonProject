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
    public partial class frm_PrinterTemplateBrwUC : UserControl
    {
        public frm_PrinterTemplateBrwUC()
        {
            InitializeComponent();
            
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

        public async Task<string> ReturnTemplateType()
        {
            int intRowID = -1;
            string intRecID = "";
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = await GeneralFunctions.GetCellValue1(intRowID, grdTenderTypes, colType);
            return intRecID;
        }

        public async Task<string> ReturnTemplateName()
        {
            int intRowID = -1;
            string intRecID = "";
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = await GeneralFunctions.GetCellValue1(intRowID, grdTenderTypes, colName);
            return intRecID;
        }

        public async Task<string> ReturnTemplateSize()
        {
            int intRowID = -1;
            string intRecID = "";
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = await GeneralFunctions.GetCellValue1(intRowID, grdTenderTypes, colSize);
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
            PosDataObject.ReceiptTemplate objDTenderTypes = new PosDataObject.ReceiptTemplate();
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
            frm_ReceiptTemplateDlg frm_TenderTypesDlg = new frm_ReceiptTemplateDlg();
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
            frm_ReceiptTemplateDlg frm_TenderTypesDlg = new frm_ReceiptTemplateDlg();
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
                if (DocMessage.MsgDelete("Printer Template"))
                {
                    string templatetype = await ReturnTemplateType();
                    string activefieldname = "";
                    
                    if (templatetype == "Receipt") activefieldname = "ReceiptTemplate";
                    if (templatetype == "Layaway") activefieldname = "LayawayTemplate";
                    if (templatetype == "Rent Issue") activefieldname = "RentIssueTemplate";
                    if (templatetype == "Return Rent Item") activefieldname = "RentReturnTemplate";
                    if (templatetype == "Repair In") activefieldname = "RepairInTemplate";
                    if (templatetype == "Repair Deliver") activefieldname = "RepairDeliverTemplate";
                    if (templatetype == "WorkOrder") activefieldname = "WorkOrderTemplate";
                    if (templatetype == "Suspend Receipt") activefieldname = "SuspendReceiptTemplate";
                    if (templatetype == "Closeout") activefieldname = "CloseoutTemplate";
                    if (templatetype == "No Sale") activefieldname = "NoSaleTemplate";
                    if (templatetype == "Paid Out") activefieldname = "PaidOutTemplate";
                    if (templatetype == "Paid In") activefieldname = "PaidInTemplate";
                    if (templatetype == "Safe Drop") activefieldname = "SafeDropTemplate";
                    if (templatetype == "Lotto Payout") activefieldname = "LottoPayoutTemplate";
                    if (templatetype == "Customer Label") activefieldname = "CustomerLabelTemplate";

                    if (GeneralFunctions.GetRecordCountForDelete("ReceiptTemplateActive", activefieldname, intRecdID) > 0)
                    {
                        DocMessage.MsgInformation("Can not delete this Template as it is set Active");
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDeleteTemplate(intRecdID) > 0)
                    {
                        DocMessage.MsgInformation("Can not delete this Template as it is linked in Item Printers");
                        return;
                    }

                    PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
                    objTenderTypes.Connection = new SqlConnection(SystemVariables.ConnectionString);

                    int ret = objTenderTypes.DeletePrinterTemplate(intRecdID);
                    
                    if (ret != 0)
                    {
                        //DocMessage.ShowException("Deleting Tender Type", strError);
                    }
                    if (templatetype == "Label - 1 Up")
                    {
                        try
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp", intRecdID.ToString());
                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (templatetype == "Label - 2 Up")
                    {
                        try
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("TwoUp", intRecdID.ToString());
                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (templatetype == "Label - Butterfly")
                    {
                        try
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Butterfly", intRecdID.ToString());
                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (templatetype == "Label - Avery 5160 / NEBS 12650")
                    {
                        try
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery5160", intRecdID.ToString());
                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (templatetype == "Label - Avery 8195")
                    {
                        try
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery8195", intRecdID.ToString());
                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                        catch
                        {

                        }
                    }



                    FetchData();
                    if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                    
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
            //UpdateOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder)), "P");
            FetchData();
            await SetCurrentRow(intRowID);
            
        }

        private async void Btndown_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            //UpdateOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTenderTypes, colorder)),
             //   "N");
            FetchData();
            await SetCurrentRow(intRowID);
            
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
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
            frm_ReceiptTemplateDlg frm_TenderTypesDlg = new frm_ReceiptTemplateDlg();
            try
            {
                frm_TenderTypesDlg.ID = await ReturnRowID();
                if (frm_TenderTypesDlg.ID > 0)
                {
                    frm_TenderTypesDlg.TemplateType = await ReturnTemplateType();
                    frm_TenderTypesDlg.TemplateName = await ReturnTemplateName();
                    frm_TenderTypesDlg.TemplateSize = await ReturnTemplateSize();

                    frm_TenderTypesDlg.ShowDialog();
                    
                }
            }
            finally
            {
                frm_TenderTypesDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            //await SetCurrentRow(intNewRecID);
        }


        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Printer Template")
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
                txtSearchGrdData.InfoText = "Search Printer Template";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Printer Template") return;
            if (txtSearchGrdData.Text == "")
            {
                grdTenderTypes.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdTenderTypes.FilterString = "([TemplateName] LIKE '%" + filterValue + "%')";
            }
        }

        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            if ((grdTenderTypes.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PrinterTemplateActivationDlg frm_TenderTypesDlg = new frm_PrinterTemplateActivationDlg();
            try
            {
                frm_TenderTypesDlg.ShowDialog();
            }
            finally
            {
                frm_TenderTypesDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
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
