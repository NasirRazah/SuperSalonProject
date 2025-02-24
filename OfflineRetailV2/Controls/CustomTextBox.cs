// Sam Park @ Mental Code Master 2019
//--
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OfflineRetailV2.Controls
{
    internal class CustomTextBox : TextBox
    {
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(CustomTextBox),
            new FrameworkPropertyMetadata(new CornerRadius(0), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCornerRadiusPropertyChanged)));

        public static DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(ImageSource), typeof(CustomTextBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnIconPropertyChanged)));

        public static DependencyProperty InfoTextProperty = DependencyProperty.Register(
                    "InfoText", typeof(string), typeof(CustomTextBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnInfoTextPropertyChanged)));

        static CustomTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata(typeof(CustomTextBox)));
        }

        public CustomTextBox() : base()
        {
            LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(Text))
                {
                    if (InfoTextBlock != null)
                    InfoTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    if (InfoTextBlock != null)
                        InfoTextBlock.Visibility = Visibility.Collapsed;
                }
            };

            PreviewMouseLeftButtonDown += (s, e) =>
            {
                if (string.IsNullOrEmpty(Text))
                {
                    if (InfoTextBlock != null)
                        InfoTextBlock.Visibility = Visibility.Collapsed;
                }
            };

            TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(Text))
                {
                    if (InfoTextBlock != null)
                        InfoTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    if (InfoTextBlock != null)
                        InfoTextBlock.Visibility = Visibility.Collapsed;
                }
            };
            GotFocus += (s, e) =>
            {
                SelectAll();
            };
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string InfoText
        {
            get => (string)GetValue(InfoTextProperty);
            set => SetValue(InfoTextProperty, value);
        }

        private TextBlock InfoTextBlock { get; set; }

        public override void OnApplyTemplate()
        {
            InfoTextBlock = GetTemplateChild("InfoTextBlock") as TextBlock;
            InfoTextBlock.FontSize = FontSize - 2;

            base.OnApplyTemplate();
        }

        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox c)
            {
                if (e.NewValue is CornerRadius cr)
                {
                    c.CornerRadius = cr;
                }
            }
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox c = d as CustomTextBox;
            c.Icon = (ImageSource)e.NewValue;
        }

        private static void OnInfoTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox c)
            {
                if (e.NewValue is string str)
                {
                    c.InfoText = str;
                }
            }
        }
    }
}