using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Auto_Login
{
    public static class ApplicationStartup
    {

        public static AutoLoginViewModel StartupApplication()
        {
            SetApplicationData(LoadUserData());
            AutoLoginViewModel viewModel = new AutoLoginViewModel();
            return viewModel;
        }

        private static UserDataType LoadUserData()
        {
            return SaveAndLoadData.LoadUserData();
        }

        private static void SetApplicationData(UserDataType data)
        {
            ApplicationData.UserData = data;
        }

    }
}
