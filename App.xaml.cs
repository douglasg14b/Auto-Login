using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace Auto_Login
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //AutoLoginViewModel autoLoginVM = ApplicationStartup.StartupApplication();

            //MainWindow nmainWindow = new MainWindow();
            //MainWindow.DataContext = autoLoginVM;
            //autoLoginVM.window = nmainWindow;
            //MainWindow.Show();

            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // now set the Cyan accent and dark theme
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Cyan"),
                                        ThemeManager.GetAppTheme("BaseLight"));

            AutoLoginViewModel autoLoginVM = ApplicationStartup.StartupApplication();
            MainWindowMetro window = new MainWindowMetro();
            window.DataContext = autoLoginVM;
            autoLoginVM.window = window;

            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                //SaveAndLoadData.SaveUserData(ApplicationData.UserData);
               // MainWindowLogic.KillDriverProcess();
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}
