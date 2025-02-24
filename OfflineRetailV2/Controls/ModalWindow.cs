// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfflineRetailV2.Controls
{
    public class ModalWindow : ContentControl
    {
        private Button CloseButton;

        static ModalWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalWindow), new FrameworkPropertyMetadata(typeof(ModalWindow)));
        }

        public ModalWindow() : base()
        {
        }

        public CommandBase CloseCommand { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CloseButton = GetTemplateChild("CloseButton") as Button;
            //CloseButton.Focusable = false;
            //CloseButton.IsTabStop = false;

            CloseButton.Click -= CloseButton_Click;
            CloseButton.Click += CloseButton_Click;
            this.PreviewKeyDown += new KeyEventHandler(CustomWindow_PreviewKeyDown);
        }

        private void CustomWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Enter && e.IsToggled == true || (sender as Button) == null)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(request);
                }
            }*/
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseCommand?.Execute(sender);
        }
    }
}