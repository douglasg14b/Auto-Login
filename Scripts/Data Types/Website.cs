using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.ComponentModel;

// Website data type.

namespace Auto_Login
{
    [Serializable]
    public class Website : INotifyPropertyChanged
    {
        public Website()
        {
            RememberUsername = true;
            NewWebsite = true;
        }

        private string m_WebsiteName;
        public string WebsiteName
        {
            get { return m_WebsiteName; }
            set { m_WebsiteName = value; RaisePropertyChanged("WebsiteName"); }
        }

        private string m_WebsiteAddress;
        public string WebsiteAddress
        {
            get { return m_WebsiteAddress; }
            set { m_WebsiteAddress = value; RaisePropertyChanged("WebsiteAddress"); }
        }

        private string m_Username;
        public string Username
        {
            get 
            {
                if (!string.IsNullOrWhiteSpace(m_Username))
                {
                    return m_Username;
                }
                else
                {
                    if (UsesUsernameAndDomain)
                    {
                        return ApplicationData.UserData.Username + "@" + ApplicationData.UserData.Domains[ApplicationData.UserData.CurrentDomainIndex];
                    }
                    else
                    {
                        return ApplicationData.UserData.Username;
                    }
                }
            }
            set { m_Username = value; RaisePropertyChanged("Username"); }
        }

        [NonSerialized]
        private SecureString m_UniquePassword;
        public SecureString UniquePassword
        {
            get { return m_UniquePassword; }
            set { m_UniquePassword = value; RaisePropertyChanged("UniquePassword"); UniquePasswordSet = true; }
        }

        /*======================================
         *        ===== View  Bools =====
         * ====================================*/

        public bool UsernameChanged { get; set; }
        private bool m_UniquePasswordSet;
        public bool UniquePasswordSet
        {
            get { return m_UniquePasswordSet; }
            set { m_UniquePasswordSet = value; }
        }

        private bool m_UsesUniquePassword;
        public bool UsesUniquePassword  //If uses uses a unique password on this site
        {
            get { return m_UsesUniquePassword; }
            set { m_UsesUniquePassword = value; RaisePropertyChanged("UsesUniquePassword"); }
        }

        private bool m_LoginToSite;
        public bool LoginToSite
        {
            get { return m_LoginToSite; }
            set { m_LoginToSite = value; RaisePropertyChanged("LoginToSite"); }
        }

        private bool m_UsesUsernameAndDomain;
        public bool UsesUsernameAndDomain
        {
            get { return m_UsesUsernameAndDomain; }
            set { m_UsesUsernameAndDomain = value; RaisePropertyChanged("UsesUsernameAndDomain"); }
        }

        private bool m_RememberUsername;
        public bool RememberUsername
        {
            get { return m_RememberUsername; }
            set { m_RememberUsername = value; RaisePropertyChanged("RememberUsername"); }
        }

        public bool NewWebsite { get; set; }

        [field: NonSerialized]
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
