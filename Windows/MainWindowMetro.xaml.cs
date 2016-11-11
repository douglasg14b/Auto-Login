using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Threading.Tasks;
using System.Windows.Shapes;
using ViewModel;
using System.Threading;

namespace Auto_Login
{
    /// <summary>
    /// Interaction logic for MainWindowMetro.xaml
    /// </summary>
    public partial class MainWindowMetro : MetroWindow
    {
        AutoLoginViewModel autoLoginViewModel;

        public MainWindowMetro()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            autoLoginViewModel = (AutoLoginViewModel)FindResource("AutoLoginViewModel");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controller = await LoadingDialog();

            SaveAndLoadData.SaveUserData(ApplicationData.UserData);
            PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("Password_Textbox");
            ApplicationData.UserData.Password = passwordBox.SecurePassword;
            MainWindowLogic.LogIntoWebsites();

            await controller.CloseAsync();
        }

        //When a website row is double-clicked, routes event to ViewModel (Bad SOC Adherance)
        private void EditWebsiteRow(object sender, MouseButtonEventArgs e)
        {
            autoLoginViewModel.EditWebsite(sender, e, this);  
        }

        private void WindowActivated(object sender, EventArgs e)
        {
            autoLoginViewModel.RefreshView();
        }

        private async Task<ProgressDialogController> LoadingDialog()
        {
            return await this.ShowProgressAsync("Logging In...", "", false);
        }












        public WebsiteViewModel websiteViewModel { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("UniquePassword_TextBox");
            websiteViewModel.SaveWebsite(passwordBox.SecurePassword);
            Flyout.IsOpen = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Flyout.IsOpen = false;
        }

        private void UsesUniquePassword_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == true)
            {
                PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("UniquePassword_TextBox");
                passwordBox.IsEnabled = true;
            }
            else
            {
                PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("UniquePassword_TextBox");
                passwordBox.IsEnabled = false;
            }
        }

        private void RemoveWebsite_Button_Click(object sender, RoutedEventArgs e)
        {
            websiteViewModel.RemoveWebsite();
            Flyout.IsOpen = false;
        }
    }
}
