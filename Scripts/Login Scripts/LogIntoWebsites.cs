using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Threading;


namespace Auto_Login
{
    public class LogIntoWebsites
    {
        Website[] Websites;

        public LogIntoWebsites(Website[] sites)
        {
            Websites = sites;
        }

        public void LogIn()
        {
            //ApplicationData.WebDriver.Navigate().GoToUrl("https://doordash.atlassian.net/wiki/display/DS/Announcements");
            //NewTab();
            //ApplicationData.WebDriver.Navigate().GoToUrl("https://google.com");

            int index = 0;
            foreach (Website site in Websites)
            {
                if (!site.LoginToSite)
                {
                    index++;
                    continue;
                }
                if(site.UsesUniquePassword && site.UniquePasswordSet)
                {
                    PasswordPopup(site);
                }

                GoToWebsite(site);

                Thread.Sleep(400);

                Tuple<IWebElement, IWebElement> loginFields = RetrieveLoginFields();

                if (loginFields != null)
                {
                    EnterLogInInformation(site, loginFields.Item1, loginFields.Item2);
                    loginFields.Item2.Submit();
                }
                index++;

                if(index < Websites.Count())
                    NewTab();
            }

            //ApplicationData.WebDriver.Navigate().GoToUrl("https://doordash.atlassian.net/wiki/display/DS/Announcements");
            //Tuple<IWebElement, IWebElement> fields = RetrieveLoginFields();

            //fields.Item1.SendKeys("");
            //fields.Item2.SendKeys("");

            //fields.Item2.Submit();
            MainWindowLogic.KillDriverProcess();
        }


        private void GoToWebsite(Website site)
        {
            ApplicationData.WebDriver.Navigate().GoToUrl(site.WebsiteAddress);
        }

        //Retrieves the login fields, a bit rough, needs a rework
        private Tuple<IWebElement, IWebElement> RetrieveLoginFields()
        {
            IList<IWebElement> usernameInputFields = FindUsernameEmailField(ApplicationData.WebDriver, ApplicationData.usernameEmailInputfieldIDs);
            IList<IWebElement> passwordInputFields = FindPasswordInputField(ApplicationData.WebDriver, ApplicationData.passwordInputfieldIDs);

            if(usernameInputFields == null || passwordInputFields == null)
            {
                return null;
            }
            else
            {
                IWebElement usernameInput = FindUsernameFieldsInArray(usernameInputFields);
                IWebElement passwordInput = FindPasswordFieldInArray(passwordInputFields);

                if(usernameInput == null || passwordInput == null)
                {
                    return null;
                }
                else
                {
                    return new Tuple<IWebElement, IWebElement>(usernameInput, passwordInput);
                }
            }
        }

        //Finds the login field in an array of fields
        private IWebElement FindUsernameFieldsInArray(IList<IWebElement> fields)
        {
            if(fields.Count == 1)
            {
                return fields[0];
            }
            else if(fields.Count > 1)
            {
                foreach(IWebElement element in fields)
                {
                    string ID = element.GetAttribute("id");
                    if (ID.Contains("login", StringComparison.OrdinalIgnoreCase))
                        return element;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        private IWebElement FindPasswordFieldInArray(IList<IWebElement> fields)
        {
            if (fields.Count == 1)
            {
                return fields[0];
            }
            else if (fields.Count > 1)
            {
                foreach (IWebElement element in fields)
                {
                    string ID = element.GetAttribute("id");
                    if (ID.Contains("login", StringComparison.OrdinalIgnoreCase))
                        return element;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        private IList<IWebElement> FindUsernameEmailField(IWebDriver driver, string[] ids)
        {
            IList<IWebElement> inputfield;
            int count = 0;
            for (int i = 0; count == 0 && i < ids.Count(); i++)
            {
                inputfield = driver.FindElements(By.Name(ids[i]));
                count = inputfield.Count;
                if(inputfield.Count > 0)
                {
                    return inputfield;
                }
            }

            return null;
        }

        private IList<IWebElement> FindPasswordInputField(IWebDriver driver, string[] ids)
        {
            IList<IWebElement> inputfield;
            int count = 0;
            for (int i = 0; count == 0 && i < ids.Count(); i++)
            {
                inputfield = driver.FindElements(By.Name(ids[i]));
                count = inputfield.Count;
                if (inputfield.Count > 0)
                {
                    return inputfield;
                }
            }
            return null;
        }

        private void EnterLogInInformation(Website site, IWebElement username, IWebElement password)
        {
            username.SendKeys(site.Username);
            if(site.UsesUniquePassword)
            {
                using (SecureStringToStringMarshaler sm = new SecureStringToStringMarshaler(site.UniquePassword))
                {
                    if (sm.String != null)
                        password.SendKeys(sm.String);
                }
            }
            else
            {
                using (SecureStringToStringMarshaler sm = new SecureStringToStringMarshaler(ApplicationData.UserData.Password))
                {
                    if (sm.String != null)
                        password.SendKeys(sm.String);
                }
            }
        }

        //Creates and switches to a new tab
        private void NewTab()
        {
            IWebElement body = ApplicationData.WebDriver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Control + 't');

            IList<string> tabs = ApplicationData.WebDriver.WindowHandles;
            ApplicationData.WebDriver.SwitchTo().Window(tabs[tabs.Count - 1]);
        }

        private void PasswordPopup(Website site)
        {
            PasswordPopup window = new PasswordPopup(site);
            window.ShowDialog();
        }
    }

}
