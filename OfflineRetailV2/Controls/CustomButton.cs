// Sam Park @ Mental Code Master 2019
//--
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.Controls
{
    public class CustomButton : Button
    {
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(CustomButton),
            new FrameworkPropertyMetadata(new CornerRadius(4.8), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCornerRadiusPropertyChanged)));

        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }

        public CustomButton() : base()
        {
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomButton c)
            {
                if (e.NewValue is CornerRadius cr)
                {
                    c.CornerRadius = cr;
                }
            }
        }
    }
}