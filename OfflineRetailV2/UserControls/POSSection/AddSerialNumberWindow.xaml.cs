// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for AddSerialNumberWindow.xaml
    /// </summary>
    public partial class AddSerialNumberWindow : Window
    {
        public AddSerialNumberWindow()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }
    }
}