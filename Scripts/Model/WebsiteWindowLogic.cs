using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

//Website Model Logic

//Doesn't really do anythign yet, so..... trash?

namespace Auto_Login
{
    public static class WebsiteWindowLogic
    {
        public static Website website { get; set; }

        /*============================================================
         *          ===== New Window Initializations =====
         * ==========================================================*/

        public static void CreateWebsiteWindow(Website site, MainWindowMetro window)
        {
            if(site != null)
            {
                website = site;
            }
            else
            {
                website = new Website();
                website.WebsiteName = "NewWebsite";
                website.WebsiteAddress = "NewWebsite";
            }


            //WebsiteWindow websiteWindow = new WebsiteWindow();   --- not needed?

            WebsiteViewModel websiteViewModel = SetUpViewModel(window);


            DetermineUsername(website, websiteViewModel);
            window.websiteViewModel = websiteViewModel;

            window.Flyout.DataContext = websiteViewModel;
            window.Flyout.IsOpen = true;
        }

        private static WebsiteViewModel SetUpViewModel(MainWindowMetro window)
        {
            WebsiteViewModel viewModel = new WebsiteViewModel(website, window);
            return viewModel;
        }


        // Quick and dirty cloning method for a website object
        private static Website CreateClone(Website site)
        {
            return XMLSerialize<Website>.Deserialize(XMLSerialize<Website>.Serialize(site));
        }
        
        //Determines how the username will be displayed
        private static void DetermineUsername(Website site, WebsiteViewModel viewModel)
        {
            if(website.Username == null || website.Username == "")
            {
                if(website.UsesUsernameAndDomain)
                {
                    viewModel.Username = ApplicationData.UserData.Username + "@" + ApplicationData.UserData.Domains[ApplicationData.UserData.CurrentDomainIndex];
                }
                else
                {
                    viewModel.Username = ApplicationData.UserData.Username;
                }
            }
            viewModel.UsernameChanged(false);
        }

        /*============================================================
         *          ===== Misc Methods =====
         * ==========================================================*/

        public static void RemoveWebsite(Website site)
        {
            ApplicationData.UserData.Websites.Remove(site);
        }

        //Saves the website info to the application data
        public static void SaveViewModel(string name,
                                       string address,
                                       string username,
                                       SecureString uniquePassword,
                                       bool usesUniquePassword,
                                       bool logIntoWebsite,
                                       bool usesUsernameAndDomain,
                                       bool usernameChanged,
                                       Website site)
        {
            site.WebsiteName = name;
            site.WebsiteAddress = address;
            site.UniquePassword = uniquePassword;
            site.UsesUniquePassword = usesUniquePassword;
            site.LoginToSite = logIntoWebsite;
            site.UsesUsernameAndDomain = usesUsernameAndDomain;
            site.UsernameChanged = usernameChanged;

            if(usernameChanged)
            {
                site.Username = username;
            }
            else
            {
                //site.user
            }

            if(site.NewWebsite)
            {
                site.NewWebsite = false;
                AddNewSiteToCollection(site);
            }
        }

        private static void AddNewSiteToCollection(Website site)
        {
            ApplicationData.UserData.Websites.Add(site);
        }


    }
}
