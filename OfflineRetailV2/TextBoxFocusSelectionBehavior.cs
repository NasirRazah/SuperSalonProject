using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;
using System.Windows.Threading;

namespace OfflineRetailV2
{
    public class TextBoxFocusSelectionBehavior : Behavior<TextEdit>
    {
        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
        }

        private void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                AssociatedObject.SelectAll();
            }), DispatcherPriority.Input);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
        }

    }
}
