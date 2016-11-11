using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.ComponentModel;
using Auto_Login;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class AutoLoginViewModel : INotifyPropertyChanged
    {
        public AutoLoginViewModel()
        {
            InitializeViewModel();
        }

        public MainWindowMetro window { get; set; }

        public bool RememberDomains
        {
            get { return ApplicationData.UserData.RememberDomain; }
            set { ApplicationData.UserData.RememberDomain = value; RaisePropertyChanged("RememberDomains"); }
        }

        public bool RememberUsername
        {
            get { return ApplicationData.UserData.RememberUsername; }
            set { ApplicationData.UserData.RememberUsername = value; RaisePropertyChanged("RememberUsername"); }
        }

        public string Username
        {
            get { return ApplicationData.UserData.Username; }
            set { ApplicationData.UserData.Username = value; RaisePropertyChanged("Username"); }
        }

        public int CurrentDomainIndex
        {
            get { return ApplicationData.UserData.CurrentDomainIndex; }
            set { ApplicationData.UserData.CurrentDomainIndex = value; RaisePropertyChanged("CurrentDomainIndex"); }
        }

        public string[] Domains
        {
            get { return ApplicationData.UserData.Domains; }
            set { ApplicationData.UserData.Domains = value; RaisePropertyChanged("Domains"); }
        }

        private string m_Password;
        public SecureString Password { get; set; }

        public ObservableCollection<Website> Websites
        {
            get { return ApplicationData.UserData.Websites; }
            set { ApplicationData.UserData.Websites = value; RaisePropertyChanged("Websites"); }
        }

        public bool OpenHipChatApp
        {
            get { return ApplicationData.UserData.OpenHipChatApp; }
            set { ApplicationData.UserData.OpenHipChatApp = value; RaisePropertyChanged("OpenHipChatApp"); }
        }

        //Initializes the viewmodel by calling RaisedPropertyChanged for each property in the class
        private void InitializeViewModel()
        {
            RefreshView();
        }

        //Called when a website row in the DataGrid is double clicked. Initilizes a new Website Window.
        public void EditWebsite(object sender, MouseButtonEventArgs e, MainWindowMetro windowMetro)
        {
            DataGridRow row = sender as DataGridRow;
            Website site = row.Item as Website;

            WebsiteWindowLogic.CreateWebsiteWindow(site, windowMetro);
        }

        //Handles Saving of data
        public void SaveData()
        {

        }

        public void RefreshView()
        {
            foreach (string name in MiscMethods.GetPropertyNames(this))
            {
                RaisePropertyChanged(name);
            }
        }

        /*==========================================================
         *  Events and Methods for DataBinding and Command Binding
         * ========================================================*/

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
