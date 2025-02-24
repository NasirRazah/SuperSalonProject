using System;
using System.Threading.Tasks;
using System.Windows;
using OfflineRetailV2.Data;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using DevExpress.XtraEditors;



namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            try
            {
                
                SystemVariables.BrandName = ConfigSettings.GetBrandName();
            }
            catch
            {
                SystemVariables.BrandName = "XEPOS Retail 2024";
            }

            try
            {
                SystemVariables.SelectedTheme = ConfigSettings.GetTheme();
            }
            catch
            {
                SystemVariables.SelectedTheme = "Dark";
            }
            

            //RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
            //Timeline.SetIsEnabled(this, true);
            var splashScreenViewModel = new DXSplashScreenViewModel()
            {
                Title = SystemVariables.BrandName,
                Copyright = "",
                Subtitle = "",
                Logo = new Uri("/Resources/Icons/Logo.png", UriKind.Relative)
            };

            SplashScreenManager.Create(() => new FluentSplashScreen1(), splashScreenViewModel).ShowOnStartup(true);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            SystemVariables.SelectedTheme = "Dark";
            base.OnStartup(e);

            /*
            if (SystemVariables.SelectedTheme == "Dark")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019BlackName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowWCStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Dark/DarkTheme.xaml", UriKind.Relative)
                });
            }

            if (SystemVariables.SelectedTheme == "Light")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericLightStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowWCLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Light/LightTheme.xaml", UriKind.Relative)
                });
            }
            */

            /*
            base.OnStartup(e);

            //initialize the splash screen and set it as the application main window
            var splashScreen = new SplashScreenWindow();
            try
            {
                SystemVariables.BrandName = ConfigSettings.GetBrandName();
            }
            catch
            {
                SystemVariables.BrandName = "XEPOS Retail 2024";
            }
            splashScreen.lbBrand.Text = SystemVariables.BrandName;
            this.MainWindow = splashScreen;
            splashScreen.Show();
            */
            //in order to ensure the UI stays responsive, we need to
            //do the work on a different thread
            Task.Factory.StartNew(() =>
            {
                //we need to do the work in batches so that we can report progress
                /*for (int i = 1; i <= 100; i++)
                {
                    //simulate a part of work being done
                    System.Threading.Thread.Sleep(30);

                    //because we're not on the UI thread, we need to use the Dispatcher
                    //associated with the splash screen to update the progress bar
                    splashScreen.Dispatcher.Invoke(() => splashScreen.Progress = i);
                }*/

                //once we're done we need to use the Dispatcher
                //to create and show the main window
                this.Dispatcher.Invoke(() =>
                {
                    //initialize the main window, set it as the application main window
                    //and close the splash screen

                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                    //splashScreen.Close();
                });
            });

        }

    }
}
