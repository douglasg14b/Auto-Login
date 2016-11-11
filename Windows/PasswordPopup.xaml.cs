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
using System.Threading;
using ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro;

// Quickly throws together class to manage a password popup..

namespace Auto_Login
{
    /// <summary>
    /// Interaction logic for PasswordPopup.xaml
    /// </summary>
    public partial class PasswordPopup : MetroWindow
    {
        public PasswordPopup(Website site)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            website = site;
            SetWebsiteString();
            Thread.Sleep(150);
            Keyboard.Focus((PasswordBox)LayoutRoot.FindName("PasswordBox"));
        }

        private Website website;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Return)
            {
                Submit();
            }
        }

        private void Submit()
        {
            PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("PasswordBox");
            if(passwordBox.SecurePassword.Length > 0)
            {
                website.UniquePassword = passwordBox.SecurePassword;
                Close();
            }
        }

        private void SetWebsiteString()
        {
            TextBlock siteName = (TextBlock)LayoutRoot.FindName("WebsiteName");
            siteName.Text = website.WebsiteName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;

            SolidColorBrush brush = new SolidColorBrush();

            GlowBrush = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;

            GlowBrush = new SolidColorBrush(Colors.Magenta);
        }
    }
}
