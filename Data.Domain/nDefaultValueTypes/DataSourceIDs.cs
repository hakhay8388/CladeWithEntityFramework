using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Domain.nDefaultValueTypes
{
    public class DataSourceIDs : cBaseConstType<DataSourceIDs>
    {
        public static List<DataSourceIDs> TypeList { get; set; }

        public static DataSourceIDs UserList_CustomQuery = new DataSourceIDs(GetVariableName(() => UserList_CustomQuery), "TUserList_CustomQuery", "UserList_CustomQuery", 6, true);

		


		public string ClientComponentName { get; set; }
        public bool IsPublic { get; set; }

        public DataSourceIDs(string _Code, string _ClientComponentName, string _Name, int _ID, bool _IsPublic = false)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<DataSourceIDs>();
            TypeList.Add(this);
            ClientComponentName = _ClientComponentName;
            IsPublic = _IsPublic;
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
