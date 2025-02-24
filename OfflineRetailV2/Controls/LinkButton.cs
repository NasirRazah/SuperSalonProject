// Sam Park @ Mental Code Master 2019
//--
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.Controls
{
    public class LinkButton : Button
    {
   
        static LinkButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkButton),
                new FrameworkPropertyMetadata(typeof(LinkButton)));
        }

        public LinkButton() : base()
        {
        }
    }
}