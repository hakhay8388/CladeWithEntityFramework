using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Domain.nDefaultValueTypes
{
    public class MenuTypes : cBaseConstType<MenuTypes>
    {
        public static List<MenuTypes> TypeList { get; set; }

        public static MenuTypes LeftMenu = new MenuTypes(GetVariableName(() => LeftMenu), "LeftMenu", "cui-list", 100);
        public static MenuTypes CenterMenu = new MenuTypes(GetVariableName(() => CenterMenu), "MiddleMenu", "cui-list", 100);

        public string Icon { get; set; }


        public MenuTypes(string _Code, string _Name, string _Icon, int _SortValue)
            : base(_Name, _Code, _SortValue)
        {
            TypeList = TypeList ?? new List<MenuTypes>();
            TypeList.Add(this);
            Icon = _Icon;
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static MenuTypes GetByID(int _ID, MenuTypes _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static MenuTypes GetByName(string _Name, MenuTypes _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static MenuTypes GetByCode(string _Code, MenuTypes _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
