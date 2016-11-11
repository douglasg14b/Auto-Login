using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Holds the data for the application

namespace Auto_Login
{
    public static class ApplicationData
    {
        public static UserDataType UserData { get; set; }

        public static IWebDriver WebDriver { get; set; }

        public static string[] usernameEmailInputfieldIDs = new string[11]
        {
            "username",
            "Username",
            "User",
            "user",
            "User_Login",
            "user_Login",
            "User_login",
            "user_login",
            "email",
            "Email",
            "user[email]"
        };

        public static string[] passwordInputfieldIDs = new string[6]
        {
            "password",
            "Password",
            "passwd",
            "passwd_login",
            "password_login",
            "user[password]"
        };
    }
}
