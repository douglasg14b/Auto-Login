using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Auto_Login
{
    public static class SaveAndLoadData
    {
        public static UserDataType LoadUserData()
        {
            CheckIfDataFolderExists();
            CheckIfDataExists();
            //UserDataType data = BinarySerialize<UserDataType>.DeserializeFromByteArray<UserDataType>(FileStreamReadWrite.ReadFromFileBinary());
            UserDataType data = BinarySerialize<UserDataType>.DeserializeFromString<UserDataType>(FileStreamReadWrite.ReadFromFile("Data\\UserData"));
            data.Domains = GetDomains();
            return data;
           //return  XMLSerialize<UserDataType>.Deserialize(FileStreamReadWrite.ReadFromFileXML());
        }

        public static void SaveUserData(UserDataType data)
        {
            UserDataType dataClone = XMLSerialize<UserDataType>.Deserialize(XMLSerialize<UserDataType>.Serialize(data));

            //string stuff = BinarySerialize<UserDataType>.SerializeToString<UserDataType>(data);
            //UserDataType stufftest = BinarySerialize<UserDataType>.DeserializeFromString<UserDataType>(stuff);

            FileStreamReadWrite.WriteToFile(BinarySerialize<UserDataType>.SerializeToString<UserDataType>(data), "Data\\UserData");

            //FileStreamReadWrite.WriteToFileBinary(BinarySerialize<UserDataType>.SerializeToBytes<UserDataType>(data));
            //FileStreamReadWrite.WriteToFile(XMLSerialize<UserDataType>.Serialize(ModifyData(dataClone)));
        }

        //Necessary to return the type? It is just a referance so it gets changed anyways
        private static UserDataType ModifyData(UserDataType type)
        {
            if(!type.RememberDomain)
            {
                type.CurrentDomainIndex = -1;
            }

            if(!type.RememberUsername)
            {
                type.Username = "";
            }

            foreach(Website site in type.Websites)
            {
                ModifyWebsiteData(site);
            }

            return type;
        }

        //Erases username and apssword data if necessary before serialization
        private static void ModifyWebsiteData(Website site)
        {
            if(!site.RememberUsername)
            {
                site.Username = "";
            }
            if(site.UniquePassword != null)
            {
                site.UniquePassword.Clear();
            }
        }

        private static string[] GetDomains()
        {
            string data = FileStreamReadWrite.ReadFromFile("Data\\Domains.txt");

            return data.Split(',');
        }

        private static void CheckIfDataFolderExists()
        {
            bool exists = Directory.Exists("Data");
            if(!exists)
            {
                Directory.CreateDirectory("Data");
            }
        }

        //Checks if application data exists, if it does not then create it
        private static void CheckIfDataExists()
        {
            bool saveExists = File.Exists("Data\\UserData");
            bool domainsExist = File.Exists("Data\\Domains.txt");
            if (!saveExists)
            {
                CreateAppDataOnDisk("savedata");
            }
            if(!domainsExist)
            {
                CreateAppDataOnDisk("domains");
            }
        }

        private static void CreateAppDataOnDisk(string type)
        {
            if(type == "savedata")
            {
                UserDataType newType = new UserDataType();
                newType.Websites = new ObservableCollection<Website>();
                FileStreamReadWrite.WriteToFile(BinarySerialize<UserDataType>.SerializeToString<UserDataType>(newType), "Data\\UserData");
            }
            else if(type == "domains")
            {
                string empty = string.Empty;
                FileStreamReadWrite.WriteToFile(empty, "Data\\Domains.txt");
            }
        }
    }
}
