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
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSNotesDlg.xaml
    /// </summary>
    public partial class frm_POSEntryTicketDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_POSEntryTicketDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSNotesDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }


        private void OnClose(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            DialogResult = false;
            Close();
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



        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();

            }

        }



        private void Frm_POSNotesDlg_Loaded(object sender, RoutedEventArgs e)
        {
            txtNotes.Text = strNotes;
            txtNotes.Focus();
            fkybrd = new FullKeyboard();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private string strNotes;
        public string Notes
        {
            get { return strNotes; }
            set { strNotes = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtNotes.Text != null)
            {
                strNotes = txtNotes.Text.Trim();
            }
            else
            {
                strNotes = "";
            }
            CloseKeyboards();
            ResMan.closeKeyboard();
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            CloseKeyboards();
            ResMan.closeKeyboard();
            DialogResult = false;
            Close();
        }
    }
}
