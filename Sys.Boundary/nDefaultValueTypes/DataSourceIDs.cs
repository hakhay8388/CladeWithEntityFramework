using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Boundary.nDefaultValueTypes
{
    public class DataSourceIDs : cBaseConstType<DataSourceIDs>
    {
        public static List<DataSourceIDs> TypeList { get; set; }

        public static DataSourceIDs UserList = new DataSourceIDs(GetVariableName(() => UserList), "TUserList", "UserList", 6, new List<RoleIDs>() { RoleIDs.Admin });

		public string ClientComponentName { get; set; }
        public bool IsPublic { get; set; }

        public List<RoleIDs> MainRoles { get; set; }

        public DataSourceIDs(string _Code, string _ClientComponentName, string _Name, int _ID, List<RoleIDs> _MainRoles)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<DataSourceIDs>();
            TypeList.Add(this);
            ClientComponentName = _ClientComponentName;
            MainRoles = _MainRoles;
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static DataSourceIDs GetByID(int _ID, DataSourceIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static DataSourceIDs GetByName(string _Name, DataSourceIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static DataSourceIDs GetByCode(string _Code, DataSourceIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
