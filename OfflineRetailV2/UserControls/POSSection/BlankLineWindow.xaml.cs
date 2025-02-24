// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for BlankLineWindow.xaml
    /// </summary>
    public partial class BlankLineWindow : Window
    {
        public BlankLineWindow()
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