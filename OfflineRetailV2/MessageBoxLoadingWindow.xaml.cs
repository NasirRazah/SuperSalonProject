using System;
using System.Collections.Generic;
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

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for MessageBoxLoadingWindow.xaml
    /// </summary>
    public partial class MessageBoxLoadingWindow : Window
    {
        public MessageBoxLoadingWindow()
        {
            InitializeComponent();
            Set();
        }

        public MessageBoxLoadingWindow(bool BookerCall)
        {
            InitializeComponent();
        }
        public void SetText(string caption, string text)
        {
            Title = caption;
            ContentTextBlock.Text = text;
            Set();
        }

        public void Set(/*string text, string caption, MessageBoxButton button, MessageBoxImage image*/)
        {

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

   
           // ShowDialog();


            return;// Result;
        }


    }
}
