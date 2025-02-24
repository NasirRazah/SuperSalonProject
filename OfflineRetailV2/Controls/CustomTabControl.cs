// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.Controls
{
    public class CustomTabControl : TabControl
    {
        public static readonly DependencyProperty ModeTypeProperty =
            DependencyProperty.Register("ModeType", typeof(TabControlModeType), typeof(CustomTabControl),
                new FrameworkPropertyMetadata(TabControlModeType.TabItemLineVisible, FrameworkPropertyMetadataOptions.AffectsRender, OnModeTypePropertyChanged));

        static CustomTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTabControl),
                new FrameworkPropertyMetadata(typeof(CustomTabControl)));
        }

        public CustomTabControl() : base()
        {
        }

        public TabControlModeType ModeType
        {
            get => (TabControlModeType)GetValue(ModeTypeProperty);
            set => SetValue(ModeTypeProperty, value);
        }

        private static void OnModeTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTabControl c = d as CustomTabControl;
            c.ModeType = (TabControlModeType)e.NewValue;
        }
    }
}