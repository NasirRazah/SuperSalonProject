using DevExpress.Xpf.Editors;
using OfflineRetailV2.Data;
using pos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSRepairItemInfoDlg.xaml
    /// </summary>
    public partial class frmPOSRepairItemInfoDlg : Window
    {

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;


        PopupKeyBoard userkybd;
        PopupKeyBoard_es userkybd_es;

        private string strRepairItemTag;
        private string strRepairItemSlNo;
        private DateTime dtRepairItemPurchaseDate;

        public string RepairItemTag
        {
            get { return strRepairItemTag; }
            set { strRepairItemTag = value; }
        }

        public string RepairItemSlNo
        {
            get { return strRepairItemSlNo; }
            set { strRepairItemSlNo = value; }
        }

        public DateTime RepairItemPurchaseDate
        {
            get { return dtRepairItemPurchaseDate; }
            set { dtRepairItemPurchaseDate = value; }
        }
        public frmPOSRepairItemInfoDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dtPurchase.EditValue != null)
            {
                if (dtPurchase.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation("Item Purchase Date can not be after today");
                    return;
                }
            }
            strRepairItemTag = txtTag.Text.Trim();
            strRepairItemSlNo = txtSlNo.Text.Trim();
            if (dtPurchase.EditValue == null) dtRepairItemPurchaseDate = Convert.ToDateTime(null);
            else dtRepairItemPurchaseDate = dtPurchase.DateTime;
            DialogResult = true;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void DtPurchase_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
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

    }


}
