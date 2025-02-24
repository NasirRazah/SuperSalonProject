// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfflineRetailV2.Controls
{
    public class ModalWindowWC : ContentControl
    {
        public Button CloseButton;

        static ModalWindowWC()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalWindowWC), new FrameworkPropertyMetadata(typeof(ModalWindowWC)));
        }

        public ModalWindowWC() : base()
        {
        }

        public CommandBase CloseCommand { get; set; }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

             CloseButton = GetTemplateChild("CloseButton") as Button;
            //CloseButton.Click -= CloseButton_Click1;
            //   CloseButton.Click += CloseButton_Click1;

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
        //private void CloseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var CloseButtoaan = GetTemplateChild("CloseButto1n") as Button;
        //    //  ReturnReprintDlg.CloseButton1_Click
        //    var clickedButton = sender as Button;

        //    CloseCommand?.Execute(sender);
        //}

        //private void CloseButton_Click1(object sender, RoutedEventArgs e)
        //{
        //    CloseCommand?.Execute(sender);
        //}
    }
}