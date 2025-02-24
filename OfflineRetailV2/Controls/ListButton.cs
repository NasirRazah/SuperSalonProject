// Sam Park @ Mental Code Master 2019
//--
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.Controls
{
    public class ListButton : Button
    {
        static ListButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListButton),
                new FrameworkPropertyMetadata(typeof(ListButton)));
        }

        public ListButton() : base()
        {
        }
    }
}