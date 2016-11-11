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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using MahApps.Metro.Controls;


using System.Security;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Auto_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AutoLoginViewModel viewModel;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            viewModel = (AutoLoginViewModel)FindResource("AutoLoginViewModel");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveAndLoadData.SaveUserData(ApplicationData.UserData);

            PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("Password_Textbox");
            ApplicationData.UserData.Password = passwordBox.SecurePassword;
            MainWindowLogic.LogIntoWebsites();
        }

        //When a website row is double-clicked, routes event to ViewModel (Bad SOC Adherance)
        private void EditWebsiteRow(object sender, MouseButtonEventArgs e)
        {
            //viewModel.EditWebsite(sender, e);            
        }

        void WindowActivated(object sender, EventArgs e)
        {
            viewModel.RefreshView();
        }
    }
}
