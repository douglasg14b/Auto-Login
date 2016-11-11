using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using System.Diagnostics;
using System.IO;

namespace Auto_Login
{
    public static class MainWindowLogic
    {
        public static void LogIntoWebsites()
        {
            StartupWebDriver();
            LogIntoWebsites logInToSite = new LogIntoWebsites(ApplicationData.UserData.Websites.ToArray());
            logInToSite.LogIn();
            if(ApplicationData.UserData.OpenHipChatApp)
            {
                OpenHipChatExtension();
            }
            KillDriverProcess();
        }

        //Required driver shenanigans due to bug in library
        private static void OpenHipChatExtension()
        {
            KillDriverProcess();


            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService("Data");
            driverService.HideCommandPromptWindow = true;

            string path = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\Google\\Chrome\\User Data");

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--app-id=gdinjfpedfbnhfpmcpaamgijaicchmod");
            options.AddArgument("user-data-dir=" + path);
            options.LeaveBrowserRunning = true;
            try
            {
                IWebDriver driver = new ChromeDriver(driverService, options, new TimeSpan(0,0, 5));
            }
            catch
            {
                KillDriverProcess();
            }
            finally
            {
                KillDriverProcess();
            }
        }

        public static void StartupWebDriver()
        {
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            //driverService.LogPath = "chromedriver5.log";
            //driverService.EnableVerboseLogging = true;


            string userDataPath = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\Google\\Chrome\\User Data");
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--app-id=gdinjfpedfbnhfpmcpaamgijaicchmod");
            options.AddArgument("user-data-dir=" + userDataPath);
            options.LeaveBrowserRunning = true;
            IWebDriver driver = new ChromeDriver(driverService, options);
            ApplicationData.WebDriver = driver;
        }

        public static void KillDriverProcess()
        {
            try
            {
                Process[] driverProcess = Process.GetProcessesByName("chromedriver");
                driverProcess[0].Kill();
            }
            catch { }
        }
    }
}
