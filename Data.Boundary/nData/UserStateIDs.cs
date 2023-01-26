using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public class UserStateIDs : cBaseConstType<UserStateIDs>
    {

        public static List<UserStateIDs> TypeList { get; set; }

        public static UserStateIDs Active = new UserStateIDs(GetVariableName(() => Active), 1);
        public static UserStateIDs NotActived = new UserStateIDs(GetVariableName(() => NotActived), 2);
        public static UserStateIDs Suspended = new UserStateIDs(GetVariableName(() => Suspended), 3);


        public UserStateIDs(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<UserStateIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static UserStateIDs GetByID(int _ID, UserStateIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static UserStateIDs GetByName(string _Name, UserStateIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static UserStateIDs GetByCode(string _Code, UserStateIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
