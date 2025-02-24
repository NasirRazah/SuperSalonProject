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
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSCustNotesBrw.xaml
    /// </summary>
    public partial class frm_POSCustNotesBrw : Window
    {
        private string strScreenNotes;
        public string ScreenNotes
        {
            get { return strScreenNotes; }
            set { strScreenNotes = value; }
        }

        private DataTable dtblNotes;
        public DataTable Notes
        {
            get { return dtblNotes; }
            set { dtblNotes = value; }
        }
        public frm_POSCustNotesBrw()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (strScreenNotes == "")
            {
            }/* pnlScreenNotes.Width = 0;*/
            else
            {
                if (dtblNotes.Rows.Count == 0)
                {

                    grdNotes.Height = 1;
                    grdNotes.Visibility = Visibility.Collapsed;
                    //pnlNotes.Width = 5;
                    btnUp.Visibility = btnDown.Visibility = Visibility.Collapsed;
                    //pnlScreenNotes.Width = 240;
                }
            }
            lbCustNotes.Text = strScreenNotes;
            grdNotes.ItemsSource = dtblNotes;
        }
    }
}
