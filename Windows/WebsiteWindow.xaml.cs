using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
using ViewModel;

namespace Auto_Login
{
    /// <summary>
    /// Interaction logic for WebsiteWindow.xaml
    /// </summary>
    public partial class WebsiteWindow : Window
    {
        public WebsiteWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public WebsiteViewModel viewModel { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)LayoutRoot.FindName("UniquePassword_TextBox");
            viewModel.SaveWebsite(passwordBox.SecurePassword);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UsesUniquePassword_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if(checkBox.IsChecked == true)
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
            viewModel.RemoveWebsite();
            Close();
        }
    }
}
