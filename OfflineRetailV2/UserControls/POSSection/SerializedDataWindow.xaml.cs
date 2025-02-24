// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for SerializedDataWindow.xaml
    /// </summary>
    public partial class SerializedDataWindow : Window
    {
        public SerializedDataWindow()
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