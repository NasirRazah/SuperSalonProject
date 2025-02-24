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
using System.Data;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_PortSetupDlg.xaml
    /// </summary>
    public partial class frm_PortSetupDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private string pCOMPort;
        private string pBaudRate;
        private string pDataBits;
        private string pStopBits;
        private string pParity;
        private string pHandshake;
        private string pTimeout;

        public string COMPort
        {
            get { return pCOMPort; }
            set { pCOMPort = value; }
        }
        public string BaudRate
        {
            get { return pBaudRate; }
            set { pBaudRate = value; }
        }
        public string DataBits
        {
            get { return pDataBits; }
            set { pDataBits = value; }
        }
        public string StopBits
        {
            get { return pStopBits; }
            set { pStopBits = value; }
        }
        public string Parity
        {
            get { return pParity; }
            set { pParity = value; }
        }
        public string Handshake
        {
            get { return pHandshake; }
            set { pHandshake = value; }
        }

        public string Timeout
        {
            get { return pTimeout; }
            set { pTimeout = value; }
        }

        public frm_PortSetupDlg()
        {
            InitializeComponent();
    
            Loaded += Frm_PortSetupDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            CloseKeyboards();
            DialogResult = false;
            Close();
        }

        private void Frm_PortSetupDlg_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            DataTable dtblSB = new DataTable();
            dtblSB.Columns.Add("None");
            dtblSB.Columns.Add("One");
            dtblSB.Columns.Add("OnePointFive");
            dtblSB.Columns.Add("Two");
            txtstopbit.ItemsSource = dtblSB;
            dtblSB.Dispose();

            DataTable dtblP = new DataTable();
            dtblP.Columns.Add("Even");
            dtblP.Columns.Add("Mark");
            dtblP.Columns.Add("None");
            dtblP.Columns.Add("Odd");
            dtblP.Columns.Add("Space");
            txtparity.ItemsSource = dtblP;
            dtblP.Dispose();

            DataTable dtblHS = new DataTable();
            dtblHS.Columns.Add("None");
            dtblHS.Columns.Add("RequstToSend");
            dtblHS.Columns.Add("RequstToSendXOnXOff");
            dtblHS.Columns.Add("XOnXOff");
            txtHandshake.ItemsSource = dtblHS;
            dtblHS.Dispose();


            txtbaud.Text = pBaudRate;
            txtdatabit.Text = pDataBits;
            txtstopbit.EditValue = pStopBits;
            txtparity.EditValue = pParity;
            txtHandshake.EditValue = pHandshake;
            txttmout.Text = pTimeout;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            pTimeout = txttmout.Text;
            pBaudRate = txtbaud.Text;
            pDataBits = txtdatabit.Text;
            pStopBits = txtstopbit.EditValue.ToString();
            pParity = txtparity.EditValue.ToString();
            pHandshake = txtHandshake.EditValue.ToString();
            CloseKeyboards();
            DialogResult =true;
            Close();
        }

        private void Txtstopbit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            DialogResult = false;
            Close();
        }
    }
}
