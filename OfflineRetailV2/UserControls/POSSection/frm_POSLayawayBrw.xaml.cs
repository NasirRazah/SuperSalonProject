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
    /// Interaction logic for frm_POSLayawayBrw.xaml
    /// </summary>
    public partial class frm_POSLayawayBrw : Window
    {
        public frm_POSLayawayBrw()
        {
            InitializeComponent();
            frm_POSLayawayBrwUC.CloseWindow = new CloseWindowCallback(() =>
            {
                ResMan.closeKeyboard();
                Close();
            });

            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            ResMan.closeKeyboard();
            Close();
        }

        private void OnCloseCOmmand(object obj)
        {
            ResMan.closeKeyboard();
            Close();
        }

        /*
        frm_POSLayawayBrwUC.CloseWindow = new CloseWindowCallback(() =>
              {
            Close();
        });*/

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Layaway;
            frm_POSLayawayBrwUC.Load();
        }
    }
}
