using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Domain.nDefaultValueTypes
{
    public class MenuIDs : cBaseConstType<MenuIDs>
    {
        public static List<MenuIDs> TypeList { get; set; }


        //////////// Global Pages ////////////

        public static MenuIDs MainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => MainPage), "MainPage", "fas fa-home", 0, true);
        public static MenuIDs LoginPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LoginPage), "LoginPage", "cui-list", 20, true);
        ///////////////////////////////////////

        //////////// Customer Pages ////////////

        public static MenuIDs UserMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => UserMainPage), "HomePage", "fas fa-home", 11, false);

        ///////////////////////////////////////

        //////////// Admin Pages ////////////

        public static MenuIDs AdminMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => AdminMainPage), "HomePage", "fas fa-home", 5, false);
        public static MenuIDs BatchJobPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => BatchJobPage), "BatchJobPage", "fas fa-network-wired", 100, false);
        public static MenuIDs ConfigurationPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => ConfigurationPage), "ConfigurationPage", "fas fa-cogs", 100, false);
        public static MenuIDs LanguagePage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LanguagePage), "LanguagePage", "fas fa-language", 102, false);
        
        public static MenuIDs UsersMenu = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => UsersMenu), "UsersMenu", "fas fa-users", 100, false);
        public static MenuIDs UserList = new MenuIDs(MenuIDs.UsersMenu, MenuTypes.CenterMenu, GetVariableName(() => UserList), "UserList", "fas fa-users", 22, false);

        ///////////////////////////////////////


        //////////// Developer Menus ////////////
        public static MenuIDs DeveloperMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => DeveloperMainPage), "HomePage", "fas fa-home", 11, false);
        public static MenuIDs SharedSessionPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => SharedSessionPage), "SharedSessionPage", "fas fa-terminal", 11, false);
        public static MenuIDs SystemSettingsPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => SystemSettingsPage), "SystemSettingsPage", "fas fa-terminal", 11, false);
        public static MenuIDs LiveSessionsPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LiveSessionsPage), "LiveSessionsPage", "fas fa-terminal", 11, false);

        ///////////////////////////////////////







        public MenuTypes MenuType { get; set; }
        public string Icon { get; set; }

        public MenuIDs RootMenu { get; set; }

		public bool IsMainMenu { 
			get
			{
				List<MenuIDs>  __SubMenu = TypeList.Where(__Item => __Item.RootMenu != null && __Item.RootMenu.Code == this.Code).ToList();
				return __SubMenu.Count > 0;
			}
		}

		public bool UnloginedPage { get; set; }


        public MenuIDs(MenuIDs _RootMenu, MenuTypes _MenuType, string _Code, string _Name, string _Icon, int _SortValue, bool _UnloginedPage)
            : base(_Name, _Code, _SortValue)
        {
            TypeList = TypeList ?? new List<MenuIDs>();
            TypeList.Add(this);
            RootMenu = _RootMenu;
            MenuType = _MenuType;
            Icon = _Icon;
            UnloginedPage = _UnloginedPage;
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static MenuIDs GetByID(int _ID, MenuIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static MenuIDs GetByName(string _Name, MenuIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static MenuIDs GetByCode(string _Code, MenuIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }

/*        public static List<MenuIDs> GetSubMenus(MenuIDs _RootMenu)
        {
	        return TypeList.Where(__Item => __Item.RootMenu = !null && __Item.RootMenu.ID == _RootMenu.ID).ToList()();
	     
        }*/

        }
    }
