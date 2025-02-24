// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        private MessageBoxButton messageBoxButton = MessageBoxButton.OK;

        static MessageBoxWindow()
        {
            Result = MessageBoxResult.None;
        }

        public MessageBoxWindow()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            Close();
        }

        private static MessageBoxResult Result { get; set; }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton button, MessageBoxImage image, Grid blur = null /*, bool? whichButtonIsDefault = null*/)
        {
            Title = caption;
            TitleTextBlock.Text = Title;
            ContentTextBlock.Text = text;


            if (ContentTextBlock.Text.Length <= 69)
            {
                ContentTextBlock.SetValue(FontSizeProperty, 24.0);
            }
            else if ((ContentTextBlock.Text.Length >= 70) && (ContentTextBlock.Text.Length <= 90))
            {
                ContentTextBlock.SetValue(FontSizeProperty, 22.0);
            }
            else if ((ContentTextBlock.Text.Length >= 91) && (ContentTextBlock.Text.Length <= 110))
            {
                ContentTextBlock.SetValue(FontSizeProperty, 20.0);
            }
            else if ((ContentTextBlock.Text.Length >= 111) && (ContentTextBlock.Text.Length <= 130))
            {
                ContentTextBlock.SetValue(FontSizeProperty, 18.0);
            }
            else if ((ContentTextBlock.Text.Length >= 131) && (ContentTextBlock.Text.Length <= 150))
            {
                ContentTextBlock.SetValue(FontSizeProperty, 16.0);
            }
            else
            {
                ContentTextBlock.SetValue(FontSizeProperty, 14.0);
            }

            LeftButton.Visibility = RightButton.Visibility = CenterButton.Visibility = Visibility.Collapsed;
            LeftButton.IsDefault = RightButton.IsDefault = false;
            Grid.SetColumn(LeftButton, 0);
            Grid.SetColumn(CenterButton, 4);
            Grid.SetColumn(RightButton, 8);
            Grid.SetColumnSpan(LeftButton, 3);
            Grid.SetColumnSpan(CenterButton, 3);
            Grid.SetColumnSpan(RightButton, 3);

            messageBoxButton = button;

            switch (button)
            {
                case MessageBoxButton.OK:
                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = "OK";
                    RightButton.IsDefault = true;
                    Grid.SetColumn(RightButton, 2);
                    Grid.SetColumnSpan(RightButton, 7);
                    RightButton.Width = 120;
                    break;

                case MessageBoxButton.OKCancel:
                    RightButton.Content = "OK"; LeftButton.Content = "CANCEL";
                    LeftButton.Visibility = RightButton.Visibility = Visibility.Visible;

                    RightButton.IsDefault = true;
                    Grid.SetColumn(LeftButton, 0);
                    Grid.SetColumn(RightButton, 6);
                    Grid.SetColumnSpan(LeftButton, 5);
                    Grid.SetColumnSpan(RightButton, 5);
                    //if (whichButtonIsDefault == true) LeftButton.IsDefault = true;
                    //else RightButton.IsDefault = true;
                    break;

                case MessageBoxButton.YesNo:
                    CenterButton.Content = "CANCEL" ; RightButton.Content = "CONFIRM";
                    CenterButton.Visibility = RightButton.Visibility = Visibility.Visible;
                    RightButton.IsDefault = true;

                    if (text == "Do you want to void order and exit?")
                    {
                        CenterButton.Content = "GO BACK"; RightButton.Content = "YES";

                        //Grid.SetColumn(CenterButton, 0);
                        //Grid.SetColumn(RightButton, 6);
                        //Grid.SetColumnSpan(CenterButton, 5);
                        //Grid.SetColumnSpan(RightButton, 5);
                    }
                    //else    //  swap cancel and confirm buttons
                    //{
                    Grid.SetColumn(RightButton, 0);
                    Grid.SetColumn(CenterButton, 6);
                    Grid.SetColumnSpan(RightButton, 5);
                    Grid.SetColumnSpan(CenterButton, 5);
                    //}

                    //if (whichButtonIsDefault == true) LeftButton.IsDefault = true;
                    //else RightButton.IsDefault = true;
                    break;

                case MessageBoxButton.YesNoCancel:
                    LeftButton.Content = "CANCEL"; CenterButton.Content = "NO"; RightButton.Content = "YES";
                    LeftButton.Visibility = RightButton.Visibility = CenterButton.Visibility = Visibility.Visible;

                    RightButton.IsDefault = true; //if (whichButtonIsDefault == true)
                    LeftButton.IsDefault = true; //else if (whichButtonIsDefault == false)
                    CenterButton.IsDefault = true; //else RightButton.IsDefault = true; 
                    break;
            }

            if (blur != null)
                blur.Visibility = Visibility.Visible;

            ShowDialog();

            if (blur != null)
                blur.Visibility = Visibility.Collapsed;

            return Result;
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton button, Grid blur = null)
        {
            MessageBoxImage image = MessageBoxImage.Information;
            switch (button)
            {
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    image = MessageBoxImage.Question;
                    break;
            }
            return Show(text, caption, button, image, blur);
        }

        private void CenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageBoxButton == MessageBoxButton.YesNoCancel) Result = MessageBoxResult.No;
            if (messageBoxButton == MessageBoxButton.YesNo) Result = MessageBoxResult.No;

            Close();
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageBoxButton == MessageBoxButton.YesNo) Result = MessageBoxResult.No;
            else if (messageBoxButton == MessageBoxButton.OKCancel) Result = MessageBoxResult.Cancel;
            else if (messageBoxButton == MessageBoxButton.YesNoCancel) Result = MessageBoxResult.Cancel;

            Close();
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageBoxButton == MessageBoxButton.YesNo) Result = MessageBoxResult.Yes;
            else if (messageBoxButton == MessageBoxButton.OKCancel) Result = MessageBoxResult.OK;
            else if (messageBoxButton == MessageBoxButton.OK) Result = MessageBoxResult.OK;
            else if (messageBoxButton == MessageBoxButton.YesNoCancel) Result = MessageBoxResult.Yes;

            Close();
        }
    }
}