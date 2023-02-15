using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Boundary.nDefaultValueTypes
{
    public class PageIDs : cBaseConstType<PageIDs>
    {

        public static List<PageIDs> TypeList { get; set; }

        /////////// Global Pages ////////////

        public static PageIDs UnloginedMainPage = new PageIDs(GetVariableName(() => UnloginedMainPage), "UnloginedMainPage", "unloginedmainpage", "TUnloginedMainPage", 0, new List<RoleIDs>() { RoleIDs .Unlogined});
        public static PageIDs LoginPage = new PageIDs(GetVariableName(() => LoginPage), "LoginPage", "login", "TLogin", 1, new List<RoleIDs>() { RoleIDs.Unlogined });


        /////////// Admin Pages 1000 - 2000////////////
        public static PageIDs AdminMainPage = new PageIDs(GetVariableName(() => AdminMainPage), "AdminMainPage", "adminmainpage", "TAdminMainPage", 1000, new List<RoleIDs>() { RoleIDs.Admin });
        public static PageIDs BatchJobPage = new PageIDs(GetVariableName(() => BatchJobPage), "BatchJobPage", "batchjobpage", "TBatchJobPage", 1001, new List<RoleIDs>() { RoleIDs.Admin });
        public static PageIDs ConfigurationPage = new PageIDs(GetVariableName(() => ConfigurationPage), "ConfigurationPage", "configurationpage", "TConfigurationPage", 1002, new List<RoleIDs>() { RoleIDs.Admin });
        public static PageIDs LanguagePage = new PageIDs(GetVariableName(() => LanguagePage), "LanguagePage", "languagepage", "TLanguagePage", 1003, new List<RoleIDs>() { RoleIDs.Admin });
        public static PageIDs UserList = new PageIDs(GetVariableName(() => UserList), "UserList", "userlist", "TUserListPage", 1004, new List<RoleIDs>() { RoleIDs.Admin });

        /// //////////////////////////////////////////


        /////////// User Pages 2000 - 3000////////////
        public static PageIDs UserMainPage = new PageIDs(GetVariableName(() => UserMainPage), "UserMainPage", "usermainpage", "TUserMainPage", 2000, new List<RoleIDs>() { RoleIDs.User });

        /// //////////////////////////////////////////



        /////////// Developer Pages 5000 - 6000////////////

        public static PageIDs DeveloperMainPage = new PageIDs(GetVariableName(() => DeveloperMainPage), "DeveloperMainPage", "developermainpage", "TDeveloperMainPage", 5000, new List<RoleIDs>() { RoleIDs.Developer });
        public static PageIDs SharedSessionPage = new PageIDs(GetVariableName(() => SharedSessionPage), "SharedSessionPage", "sharedsessionpage", "TSharedSessionPage", 5001, new List<RoleIDs>() { RoleIDs.Developer });
        public static PageIDs SystemSettingsPage = new PageIDs(GetVariableName(() => SystemSettingsPage), "SystemSettingsPage", "systemsettingspage", "TSystemSettingsPage", 5002, new List<RoleIDs>() { RoleIDs.Developer });
        public static PageIDs LiveSessionsPage = new PageIDs(GetVariableName(() => LiveSessionsPage), "LiveSessionsPage", "livesessionpage", "TLiveSessionsPage", 5003, new List<RoleIDs>() { RoleIDs.Developer });

        /// ////////////////////////////////////////// <summary>
        /// //////////////////////////////////////////
        /// </summary>



        public string Url { get; set; }
        public string Component { get; set; }
        public string[] SubParamName { get; set; }
        public string OriginalCode { get; set; }
        public List<RoleIDs> MainRoles { get; set; }

        public PageIDs(string _Code, string _Name, string _Url, string _Component, int _ID, List<RoleIDs> _MainRoles, string[] _SubParamName = null)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<PageIDs>();
            this.Url = _Url.LowerConvertToEnglishCharacter();
            this.OriginalCode = _Code;
            this.Code = this.Code.LowerConvertToEnglishCharacter();
            MainRoles = _MainRoles;
            TypeList.Add(this);
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
