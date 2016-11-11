using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.ComponentModel;

namespace Auto_Login
{
    public class LoginCredentials : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string EmailDomain { get; set; }
        public SecureString Password { get; set; }

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
