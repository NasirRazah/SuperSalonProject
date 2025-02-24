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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSProductNoteDlg.xaml
    /// </summary>
    public partial class frm_POSProductNoteDlg : Window
    {
        public frm_POSProductNoteDlg()
        {
            InitializeComponent();
            Loaded += Frm_POSProductNoteDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSProductNoteDlg_Loaded(object sender, RoutedEventArgs e)
        {
            txtNotes.Text = strNotes;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            Close();
        }
    }
}
