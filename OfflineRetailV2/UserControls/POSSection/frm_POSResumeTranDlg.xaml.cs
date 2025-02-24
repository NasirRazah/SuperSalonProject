using DevExpress.Xpf.Editors.Settings;
using Microsoft.PointOfService;
using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSResumeTranDlg.xaml
    /// </summary>
    public partial class frm_POSResumeTranDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSResumeTranDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSResumeTranDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }






        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }


        private void Frm_POSResumeTranDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            if (Settings.DecimalPlace == 3)
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
            else
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = objPOS.FetchSuspendedData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdTran.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();
            txtInv.Focus();

            if (Settings.ScaleDevice == "Datalogic Scale")
            {
                PrepareDatalogicScanner();
            }
        }

        private string SCAN = "";
        PosExplorer m_posExplorer = null;
        Scanner m_posScanner = null;

        private int intSuspendInv;
        public int SuspendInv
        {
            get { return intSuspendInv; }
            set { intSuspendInv = value; }
        }

        private string strSuspendDOB;
        public string SuspendDOB
        {
            get { return strSuspendDOB; }
            set { strSuspendDOB = value; }
        }
        private void PrepareDatalogicScanner()
        {
            SCAN = "";
            bool blFind = false;
            m_posExplorer = new PosExplorer();

            DeviceInfo deviceInfo = null;
            DeviceCollection deviceCollection = m_posExplorer.GetDevices();
            string deviceName = Settings.Datalogic_Scanner;
            for (int i = 0; i < deviceCollection.Count; i++)
            {
                deviceInfo = deviceCollection[i];
                if (deviceInfo.ServiceObjectName == deviceName)
                {
                    blFind = true;
                    break;
                }
            }

            if (blFind)
            {
                if (deviceInfo != null)
                {
                    if (m_posScanner != null) { m_posScanner.Release(); m_posScanner.Close(); }

                    try
                    {
                        m_posScanner = (Scanner)m_posExplorer.CreateInstance(deviceInfo);

                        m_posScanner.Open();
                        m_posScanner.Claim(20000);

                        m_posScanner.DeviceEnabled = true;
                        m_posScanner.DataEventEnabled = true;
                        m_posScanner.DecodeData = true;
                        m_posScanner.DataEvent += new DataEventHandler(_Scanner_DataEvent);
                    }
                    catch
                    {
                    }
                }

            }

        }

        void _Scanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();

                byte[] b = m_posScanner.ScanData;

                string str = "";

                b = m_posScanner.ScanDataLabel;
                for (int i = 0; i < b.Length; i++)
                    str += (char)b[i];

                m_posScanner.DataEventEnabled = true;
                m_posScanner.DeviceEnabled = true;

                if (Settings.Scanner_8200 == "Y")
                {
                    try
                    {
                        str = str.Remove(0, 3);
                    }
                    catch
                    {
                    }
                }

                SCAN = str;
                txtInv.Text = SCAN;
                try
                {
                    m_posScanner.DeviceEnabled = false;
                    FetchSpecificData();
                    SCAN = "";
                }
                finally
                {
                    m_posScanner.DeviceEnabled = true;
                }
            }
            catch (PosControlException)
            {

            }
            finally
            {

            }
        }

        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                intSuspendInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                strSuspendDOB = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colDOB);
                ResMan.closeKeyboard();
                CloseKeyboards();
                DialogResult =true;
            }
        }

        private async void gridView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                intSuspendInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                strSuspendDOB = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colDOB);
                ResMan.closeKeyboard();
                CloseKeyboards();
                DialogResult = true;
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_ , "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                blurGrid.Visibility = Visibility.Visible;
                frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.PrintType = "Suspend Receipt";
                    frm_POSInvoicePrintDlg.InvNo = INV;
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void txtInv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtInv.Text.Trim() == "") e.Handled = false;
                FetchSpecificData();
                e.Handled = true;
            }
        }

        private void FetchSpecificData()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = objPOS.FetchSuspendedData_Specific(GeneralFunctions.fnInt32(txtInv.Text.Trim()));
            if (dtbl.Rows.Count == 1)
            {
                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtbl.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }

                grdTran.ItemsSource = dtblTemp;

                dtblTemp.Dispose();
            }
            dtbl.Dispose();
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 1)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
            txtInv.Text = "";
            txtInv.Focus();
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == 0) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == (grdTran.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            DialogResult = false;
            Close();
        }
    }
}
