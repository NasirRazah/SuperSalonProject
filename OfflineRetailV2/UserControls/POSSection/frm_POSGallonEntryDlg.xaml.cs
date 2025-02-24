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
    /// Interaction logic for frm_POSGallonEntryDlg.xaml
    /// </summary>
    public partial class frm_POSGallonEntryDlg : Window
    {
        private double dblGallon;
        public double Gallon
        {
            get { return dblGallon; }
            set { dblGallon = value; }
        }

        public frm_POSGallonEntryDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSGallonEntryDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void Frm_POSGallonEntryDlg_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Enter_Gallons;
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dblGallon = GeneralFunctions.fnDouble(txtGallon.Text);
            if (dblGallon == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Entry);
                GeneralFunctions.SetFocus(txtGallon);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
