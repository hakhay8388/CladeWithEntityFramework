using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Boundary.nDefaultValueTypes
{
    public class MenuIDs : cBaseConstType<MenuIDs>
    {
        public static List<MenuIDs> TypeList { get; set; }


        //////////// Global Pages ////////////

        public static MenuIDs UnloginedMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => UnloginedMainPage), "fas fa-home", 0, new List<RoleIDs>() { RoleIDs.Unlogined });
        public static MenuIDs LoginPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LoginPage), "cui-list", 20, new List<RoleIDs>() { RoleIDs.Unlogined });
        ///////////////////////////////////////

        //////////// Customer Pages ////////////

        public static MenuIDs UserMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => UserMainPage), "fas fa-home", 11, new List<RoleIDs>() { RoleIDs.User });

        ///////////////////////////////////////

        //////////// Admin Pages ////////////

        public static MenuIDs AdminMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => AdminMainPage), "fas fa-home", 5, new List<RoleIDs>() { RoleIDs.Admin });
        public static MenuIDs BatchJobPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => BatchJobPage), "fas fa-network-wired", 100, new List<RoleIDs>() { RoleIDs.Admin });
        public static MenuIDs ConfigurationPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => ConfigurationPage), "fas fa-cogs", 100, new List<RoleIDs>() { RoleIDs.Admin });
        public static MenuIDs LanguagePage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LanguagePage), "fas fa-language", 102, new List<RoleIDs>() { RoleIDs.Admin });
        
        public static MenuIDs UsersMenu = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => UsersMenu),  "fas fa-users", 100, new List<RoleIDs>() { RoleIDs.Admin });
        public static MenuIDs UserList = new MenuIDs(MenuIDs.UsersMenu, MenuTypes.CenterMenu, GetVariableName(() => UserList), "fas fa-users", 22, new List<RoleIDs>() { RoleIDs.Admin });

        ///////////////////////////////////////


        //////////// Developer Menus ////////////
        public static MenuIDs DeveloperMainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => DeveloperMainPage), "fas fa-home", 11, new List<RoleIDs>() { RoleIDs.Developer });
        public static MenuIDs SharedSessionPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => SharedSessionPage), "fas fa-terminal", 11, new List<RoleIDs>() { RoleIDs.Developer });
        public static MenuIDs SystemSettingsPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => SystemSettingsPage), "fas fa-terminal", 11, new List<RoleIDs>() { RoleIDs.Developer });
        public static MenuIDs LiveSessionsPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => LiveSessionsPage), "fas fa-terminal", 11, new List<RoleIDs>() { RoleIDs.Developer });

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

        public List<RoleIDs> MainRoles { get; set; }


        public MenuIDs(MenuIDs _RootMenu, MenuTypes _MenuType, string _Code, string _Icon, int _SortValue, List<RoleIDs> _MainRoles)
            : base(_Code, _Code, _SortValue)
        {
            TypeList = TypeList ?? new List<MenuIDs>();
            TypeList.Add(this);
            RootMenu = _RootMenu;
            MenuType = _MenuType;
            Icon = _Icon;
            MainRoles = _MainRoles;
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
