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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace OfflineRetailV2.Alert
{
    
    public partial class GreenAlert : Window
    {
        private DoubleAnimation _fadeInAnimation;
        private DoubleAnimation _fadeOutAnimation;
        private DispatcherTimer _activeTimer;

        private string strMessageText;
        public string MessageText
        {
            get { return strMessageText; }
            set { strMessageText = value; }
        }

        public GreenAlert(string msg)
        {
            InitializeComponent();
            Visibility = Visibility.Visible;
            strMessageText = msg;
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
            AllowsTransparency = true;
            Opacity = 1.0;
            BorderThickness = new Thickness(1);
            BorderBrush = Brushes.Black;
            Background = Brushes.White;

            // Set up the fade in and fade out animations
            _fadeInAnimation = new DoubleAnimation();
            _fadeInAnimation.From = 0;
            _fadeInAnimation.To = 1;
            _fadeInAnimation.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));

            // For the fade out we omit the from, so that it can be smoothly initiated
            // from a fade in that gets interrupted when the user wants to close the window
            _fadeOutAnimation = new DoubleAnimation();
            _fadeOutAnimation.To = 0;
            _fadeOutAnimation.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));

            //Loaded += new RoutedEventHandler(Window_Loaded);

            lbmsg.Text = strMessageText;
            // Figure out where to place the window based on the current screen resolution
            Rect workAreaRectangle = System.Windows.SystemParameters.WorkArea;
            Left = Application.Current.MainWindow.Left + 10;
            Top = Application.Current.MainWindow.Top + Application.Current.MainWindow.Height - 70;

            _fadeInAnimation.Completed += new EventHandler(_fadeInAnimation_Completed);

            // Start the fade in animation
            BeginAnimation(GreenAlert.OpacityProperty, _fadeInAnimation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void _fadeInAnimation_Completed(object sender, EventArgs e)
        {
            _activeTimer = new DispatcherTimer();
            _activeTimer.Interval = TimeSpan.Parse("0:0:3");

            // Attach an anonymous method to the timer so that we can start fading out the alert
            // when the timer is done.
            _activeTimer.Tick += delegate (object obj, EventArgs ea) { FadeOut(); };

            _activeTimer.Start();
        }

        // Set up the fade out animation, and hook up an event handler to fire when it is completed.
        private void FadeOut()
        {
            // Attach an anonymous method to the Completed-event of the fade out animation
            // so that we can close the alert window when the animation is done.
            _fadeOutAnimation.Completed += delegate (object sender, EventArgs e) { Close(); };

            BeginAnimation(GreenAlert.OpacityProperty, _fadeOutAnimation, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
