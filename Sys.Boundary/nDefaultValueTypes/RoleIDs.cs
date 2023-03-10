using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Boundary.nDefaultValueTypes
{
    public class RoleIDs : cBaseConstType<RoleIDs>
    {

        public static List<RoleIDs> TypeList { get; set; }

        public static RoleIDs Admin = new RoleIDs(GetVariableName(() => Admin), GetVariableName(() => Admin), 1);
        public static RoleIDs User = new RoleIDs(GetVariableName(() => User), GetVariableName(() => User), 2);
		public static RoleIDs Unlogined = new RoleIDs(GetVariableName(() => Unlogined), GetVariableName(() => Unlogined), 3);
        public static RoleIDs Developer = new RoleIDs(GetVariableName(() => Developer), GetVariableName(() => Developer), 4);


        public string MainCode { get; set; }
        public RoleIDs(string _MainCode, string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            MainCode = _MainCode;
            TypeList = TypeList ?? new List<RoleIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static RoleIDs GetByID(int _ID, RoleIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static RoleIDs GetByName(string _Name, RoleIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static RoleIDs GetByCode(string _Code, RoleIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
