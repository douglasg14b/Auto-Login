using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Auto_Login;
using System.Reflection;
using System.Security;

namespace ViewModel
{
    public class WebsiteViewModel : INotifyPropertyChanged
    {
        public WebsiteViewModel(Website website, MainWindowMetro mainWindow)
        {
            WebsiteObject = website;
            this.mainWindow = mainWindow;
        }

        public WebsiteViewModel(){}

        public MainWindowMetro mainWindow { get; set; }

        private Website m_WebsiteObject;
        public Website WebsiteObject
        {
            get { return m_WebsiteObject; }
            set 
            { 
                m_WebsiteObject = value; 
                RaisePropertyChanged("WebsiteObject");
                SetViewModelProperties(); 
            }
        }

        private string m_Name;
        public string Name
        {
            get { return m_Name; }

            set { m_Name = value; RaisePropertyChanged("Name"); }
        }

        private string m_Address;
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; RaisePropertyChanged("Address"); }
        }

        private string m_Username;
        public string Username
        {
            get { return m_Username; }
            set { m_Username = value; RaisePropertyChanged("Username"); UsernameChanged(true); }
        }

        private SecureString m_UniquePassword;
        public SecureString UniquePassword
        {
            get { return m_UniquePassword; }
            set { m_UniquePassword = value; RaisePropertyChanged("UniquePassword"); }
        }

        private bool m_UsesUniquePassword;
        public bool UsesUniquePassword
        {
            get { return m_UsesUniquePassword; }
            set { m_UsesUniquePassword = value; RaisePropertyChanged("UniquePassword"); }
        }

        private bool m_LogIntoWebsite;
        public bool LogIntoWebsite
        {
            get { return m_LogIntoWebsite; }
            set { m_LogIntoWebsite = value; RaisePropertyChanged("LogIntoWebsite"); }
        }

        private bool m_RememberUsername;
        public bool RememberUsername
        {
            get { return m_RememberUsername; }
            set { m_RememberUsername = value; RaisePropertyChanged("RememberUsername"); }
        }

        private bool m_UsesUsernameAndDomain;
        public bool UsesUsernameAndDomain
        {
            get { return m_UsesUsernameAndDomain; }
            set { m_UsesUsernameAndDomain = value; RaisePropertyChanged("UsesUsernameAndDomain"); ChangeDefaultUsernames(value); }
        }

        private bool usernameChanged = false;

        /*==============================================
         *     ==== View Model Logic And Methods ====
         * ===========================================*/

        //Initializes the viewmodel by calling RaisedPropertyChanged for each property in the class
        //Depreciated for now, was an elegant solution, replaced by inelegant but differant one.
        private void InitializeViewModel()
        {
            foreach(string name in MiscMethods.GetPropertyNames(this))
            {
                RaisePropertyChanged(name);
            }
        }

        // Sets the properties values to the values of the website object upon setting
        private void SetViewModelProperties()
        {
            Name = WebsiteObject.WebsiteName;
            Address = WebsiteObject.WebsiteAddress;
            Username = WebsiteObject.Username;
            UsesUniquePassword = WebsiteObject.UsesUniquePassword;
            LogIntoWebsite = WebsiteObject.LoginToSite;
            RememberUsername = WebsiteObject.RememberUsername;
            UsesUsernameAndDomain = WebsiteObject.UsesUsernameAndDomain;

            UniquePassword = WebsiteObject.UniquePassword;
            usernameChanged = false;
        }

        //Saves Property changes to the website object
        public void SaveWebsite(SecureString password)
        {
            UniquePassword = password;
            WebsiteWindowLogic.SaveViewModel(Name, Address, Username, UniquePassword, UsesUniquePassword, LogIntoWebsite, UsesUsernameAndDomain, usernameChanged, WebsiteObject);
        }

        public void RemoveWebsite()
        {
            WebsiteWindowLogic.RemoveWebsite(WebsiteObject);
        }

        public void UsernameChanged(bool value)
        {
            usernameChanged = value;
        }

        //Called when the UsesUsernameAndDomain bool is changed
        private void ChangeDefaultUsernames(bool value)
        {
            if (value)
            {
                Username = ApplicationData.UserData.Username + "@" + ApplicationData.UserData.Domains[ApplicationData.UserData.CurrentDomainIndex];
            }
            else
            {
                Username = ApplicationData.UserData.Username;
            }
            usernameChanged = false;
        }

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
