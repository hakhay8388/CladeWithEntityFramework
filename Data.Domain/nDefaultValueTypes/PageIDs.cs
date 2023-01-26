using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDefaultValueTypes
{
    public class PageIDs : cBaseConstType<PageIDs>
    {

        public static List<PageIDs> TypeList { get; set; }

        /////////// Global Pages ////////////

        public static PageIDs MainPage = new PageIDs(GetVariableName(() => MainPage), "MainPage", "mainpage", "TMainPage", 0, true);
        public static PageIDs LoginPage = new PageIDs(GetVariableName(() => LoginPage), "LoginPage", "login", "TLogin", 0, true);

        public static PageIDs MenuPage = new PageIDs(GetVariableName(() => MenuPage), "MenuPage", "menupage", "TMenuPage", 60, false, new string[] { "menuName" });


        /////////// Admin Pages 1000 - 2000////////////
        public static PageIDs AdminMainPage = new PageIDs(GetVariableName(() => AdminMainPage), "AdminMainPage", "adminmainpage", "TAdminMainPage", 1000, false);
        public static PageIDs BatchJobPage = new PageIDs(GetVariableName(() => BatchJobPage), "BatchJobPage", "batchjobpage", "TBatchJobPage", 1001, false);
        public static PageIDs ConfigurationPage = new PageIDs(GetVariableName(() => ConfigurationPage), "ConfigurationPage", "configurationpage", "TConfigurationPage", 1002, false);
        public static PageIDs LanguagePage = new PageIDs(GetVariableName(() => LanguagePage), "LanguagePage", "languagepage", "TLanguagePage", 1003, false);
        public static PageIDs UserList = new PageIDs(GetVariableName(() => UserList), "UserList", "userlist", "TUserListPage", 1004, false);

        /// //////////////////////////////////////////


        /////////// User Pages 2000 - 3000////////////
        public static PageIDs UserMainPage = new PageIDs(GetVariableName(() => UserMainPage), "UserMainPage", "usermainpage", "TUserMainPage", 2000, false);

        /// //////////////////////////////////////////



        /////////// Developer Pages 5000 - 6000////////////

        public static PageIDs DeveloperMainPage = new PageIDs(GetVariableName(() => DeveloperMainPage), "DeveloperMainPage", "developermainpage", "TDeveloperMainPage", 5000, false);
        public static PageIDs SharedSessionPage = new PageIDs(GetVariableName(() => SharedSessionPage), "SharedSessionPage", "sharedsessionpage", "TSharedSessionPage", 5001, false);
        public static PageIDs SystemSettingsPage = new PageIDs(GetVariableName(() => SystemSettingsPage), "SystemSettingsPage", "systemsettingspage", "TSystemSettingsPage", 5002, false);
        public static PageIDs LiveSessionsPage = new PageIDs(GetVariableName(() => LiveSessionsPage), "LiveSessionsPage", "livesessionpage", "TLiveSessionsPage", 5003, false);

        /// ////////////////////////////////////////// <summary>
        /// //////////////////////////////////////////
        /// </summary>



        public string Url { get; set; }
        public string Component { get; set; }
        public string[] SubParamName { get; set; }
        public bool UnloginedPage { get; set; }
        public string OriginalCode { get; set; }

        public PageIDs(string _Code, string _Name, string _Url, string _Component, int _ID, bool _UnloginedPage, string[] _SubParamName = null)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<PageIDs>();
            this.Url = _Url.LowerConvertToEnglishCharacter();
            this.OriginalCode = _Code;
            this.Code = this.Code.LowerConvertToEnglishCharacter();
            TypeList.Add(this);

            UnloginedPage = _UnloginedPage;
            Component = _Component;
            SubParamName = _SubParamName == null ? new string[] { } : _SubParamName;

        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static PageIDs GetByID(int _ID, PageIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static PageIDs GetByName(string _Name, PageIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static PageIDs GetByCode(string _Code, PageIDs _DefaultID)
        {

            return GetByCode(TypeList, _Code.LowerConvertToEnglishCharacter(), _DefaultID);
        }
        public static PageIDs GetByUrl(string _Url)
        {
            PageIDs __Item = TypeList.Find(_Item => _Item.Url == _Url);
            return __Item;
        }
    }
}
