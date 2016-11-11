using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auto_Login;
using System.ComponentModel;
using System.Security;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(Website website)
        {
            WebsiteObject = website;
            InitializeViewModel();
        }

        /*==========================================================
         *          ****** Main Window Properties ******
         * ========================================================*/

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

        /*==========================================================
         *         ****** Flyout Window Properties ******
         * ========================================================*/

        private Website m_WebsiteObject;
        public Website WebsiteObject
        {
            get { return m_WebsiteObject; }
            set
            {
                m_WebsiteObject = value;
                RaisePropertyChanged("WebsiteObject");
                SetFlyoutViewModelProperties();
            }
        }

        private string m_NameFlyout;
        public string NameFlyout
        {
            get { return m_NameFlyout; }

            set { m_NameFlyout = value; RaisePropertyChanged("Name"); }
        }

        private string m_AddressFlyout;
        public string AddressFlyout
        {
            get { return m_AddressFlyout; }
            set { m_AddressFlyout = value; RaisePropertyChanged("Address"); }
        }

        private string m_UsernameFlyout;
        public string UsernameFlyout
        {
            get { return m_UsernameFlyout; }
            set { m_UsernameFlyout = value; RaisePropertyChanged("Username"); UsernameChanged(true); }
        }

        private SecureString m_UniquePasswordFlyout;
        public SecureString UniquePasswordFlyout
        {
            get { return m_UniquePasswordFlyout; }
            set { m_UniquePasswordFlyout = value; RaisePropertyChanged("UniquePassword"); }
        }

        private bool m_UsesUniquePasswordFlyout;
        public bool UsesUniquePasswordFlyout
        {
            get { return m_UsesUniquePasswordFlyout; }
            set { m_UsesUniquePasswordFlyout = value; RaisePropertyChanged("UniquePassword"); }
        }

        private bool m_LogIntoWebsiteFlyout;
        public bool LogIntoWebsiteFlyout
        {
            get { return m_LogIntoWebsiteFlyout; }
            set { m_LogIntoWebsiteFlyout = value; RaisePropertyChanged("LogIntoWebsite"); }
        }

        private bool m_RememberUsernameFlyout;
        public bool RememberUsernameFlyout
        {
            get { return m_RememberUsernameFlyout; }
            set { m_RememberUsernameFlyout = value; RaisePropertyChanged("RememberUsername"); }
        }

        private bool m_UsesUsernameAndDomainFlyout;
        public bool UsesUsernameAndDomainFlyout
        {
            get { return m_UsesUsernameAndDomainFlyout; }
            set { m_UsesUsernameAndDomainFlyout = value; RaisePropertyChanged("UsesUsernameAndDomain"); ChangeDefaultUsernames(value); }
        }

        private bool usernameChangedFlyout = false;

        /*==============================================
         *     ==== View Model Logic And Methods ====
         * ===========================================*/

        //Initializes the viewmodel by calling RaisedPropertyChanged for each property in the class
        //Depreciated for now, was an elegant solution, replaced by inelegant but differant one.
        private void InitializeViewModel()
        {
            foreach (string name in MiscMethods.GetPropertyNames(this))
            {
                RaisePropertyChanged(name);
            }
        }

        private void SetFlyoutViewModelProperties()
        {
            NameFlyout = WebsiteObject.WebsiteName;
            AddressFlyout = WebsiteObject.WebsiteAddress;
            UsernameFlyout = WebsiteObject.Username;
            UsesUniquePasswordFlyout = WebsiteObject.UsesUniquePassword;
            LogIntoWebsiteFlyout = WebsiteObject.LoginToSite;
            RememberUsernameFlyout = WebsiteObject.RememberUsername;
            UsesUsernameAndDomainFlyout = WebsiteObject.UsesUsernameAndDomain;

            UniquePasswordFlyout = WebsiteObject.UniquePassword;
            usernameChangedFlyout = false;
        }

        //Saves Property changes to the website object
        public void SaveWebsite(SecureString password)
        {
            UniquePasswordFlyout = password;
            WebsiteWindowLogic.SaveViewModel(NameFlyout, AddressFlyout, UsernameFlyout, UniquePasswordFlyout, UsesUniquePasswordFlyout, LogIntoWebsiteFlyout, UsesUsernameAndDomainFlyout, usernameChangedFlyout, WebsiteObject);
        }

        public void RemoveWebsite()
        {
            WebsiteWindowLogic.RemoveWebsite(WebsiteObject);
        }

        public void UsernameChanged(bool value)
        {
            usernameChangedFlyout = value;
        }

        //Called when the UsesUsernameAndDomain bool is changed
        private void ChangeDefaultUsernames(bool value)
        {
            if (value)
            {
                UsernameFlyout = ApplicationData.UserData.Username + "@" + ApplicationData.UserData.Domains[ApplicationData.UserData.CurrentDomainIndex];
            }
            else
            {
                UsernameFlyout = ApplicationData.UserData.Username;
            }
            usernameChangedFlyout = false;
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
