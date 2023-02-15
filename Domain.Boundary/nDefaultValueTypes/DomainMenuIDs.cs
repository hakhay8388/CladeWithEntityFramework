using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainMenuIDs : MenuIDs
    {
        public static MenuIDs TestPage { get; set; }

        public static void Init()
        {
            TestPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => TestPage), "fas fa-home", 0, new List<RoleIDs>() { RoleIDs.Admin });
        }

        public DomainMenuIDs(MenuIDs _RootMenu, MenuTypes _MenuType, string _Code, string _Icon, int _SortValue, List<RoleIDs> _MainRoles) 
            : base(_RootMenu, _MenuType, _Code, _Icon, _SortValue, _MainRoles)
        {
        }
    }
}
