// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using OfflineRetailV2.Data;

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frm_CustomErrorDlg.xaml
    /// </summary>
    public partial class frm_CustomErrorDlg : Window
    {
        private string strErrorMsg = "";
        private string strStackTrace = "";
        private System.Windows.Forms.SaveFileDialog svdlg;

        public frm_CustomErrorDlg()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);

            Loaded += Frm_CustomErrorDlg_Loaded;
        }

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
        }

        public string StackTrace
        {
            get { return strStackTrace; }
            set { strStackTrace = value; }
        }

        private void Frm_CustomErrorDlg_Loaded(object sender, RoutedEventArgs e)
        {
            txtDBVersion.Text = Properties.Resources.DB_Version + " :" + DbDefination.strDbVersion + "\n" + Properties.Resources.Product_Version + " : " + GeneralFunctions.VersionInfo();
            memoEdit1.Text = strStackTrace;
            lbmsg.Text = strErrorMsg;
        }

        private void OnCloseCommand(object obj)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "mailto:?subject=RW3.0/DB Version " + DbDefination.strDbVersion + " " + GeneralFunctions.VersionInfo() + " " + Title.Text + " : " + strErrorMsg + "&body=" + strStackTrace.Replace("&", "%26");
            p.Start();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Stream strm;
            svdlg = new System.Windows.Forms.SaveFileDialog();
            if (svdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((strm = svdlg.OpenFile()) != null)
                {
                    StreamWriter wText = new StreamWriter(strm);
                    wText.Write(strErrorMsg + "\n" + strStackTrace);
                    strm.Close();
                    DocMessage.MsgInformation(Properties.Resources.Error_information_saved_successfully_);
                }
            }
        }

        private void simpleButton3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}