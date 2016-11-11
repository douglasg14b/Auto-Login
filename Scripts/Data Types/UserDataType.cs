using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Login
{
    [Serializable]
    public class UserDataType : INotifyPropertyChanged
    {
        private string m_Username;
        public string Username
        {
            get { return m_Username; }
            set { m_Username = value; RaisePropertyChanged("Username"); }
        }

        private string[] m_Domains;
        public string[] Domains
        {
            get { return m_Domains; }
            set { m_Domains = value; RaisePropertyChanged("Domains"); }
        }

        private int m_CurrentDomainIndex;
        public int CurrentDomainIndex
        {
            get { return m_CurrentDomainIndex; }
            set { m_CurrentDomainIndex = value; RaisePropertyChanged("CurrentDomainIndex"); }
        }

        private bool m_RememberUsername;
        public bool RememberUsername
        {
            get { return m_RememberUsername; }
            set { m_RememberUsername = value; RaisePropertyChanged("RememberUsername"); }
        }

        private bool m_RememberDomain;
        public bool RememberDomain
        {
            get { return m_RememberDomain; }
            set { m_RememberDomain = value; RaisePropertyChanged("RememberDomain"); }
        }

        private bool m_OpenHipChatApp;
        public bool OpenHipChatApp
        {
            get { return m_OpenHipChatApp; }
            set { m_OpenHipChatApp = value; RaisePropertyChanged("OpenHipChatApp"); }
        }


        [NonSerialized]
        private SecureString m_Password;
        public SecureString Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        private ObservableCollection<Website> m_Websites;
        public ObservableCollection<Website> Websites
        {
            get { return m_Websites; }
            set{ m_Websites = value; RaisePropertyChanged("Websites"); }
        }

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
