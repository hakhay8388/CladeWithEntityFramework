using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Domain.nDefaultValueTypes
{
    public class DefaultGlobalParamsIDs : cBaseConstType<DefaultGlobalParamsIDs>
    {
        public static List<DefaultGlobalParamsIDs> TypeList { get; set; }


        public static DefaultGlobalParamsIDs TestParm1 = new DefaultGlobalParamsIDs(GetVariableName(() => TestParm1), "TestParm1", 1, "test value", 1, false);
        public static DefaultGlobalParamsIDs FrontEndDebugMessage = new DefaultGlobalParamsIDs(GetVariableName(() => FrontEndDebugMessage), "FrontEndDebugMessage", 2, false, 29, false);
        public static DefaultGlobalParamsIDs BackendDebugMessageShowToUser = new DefaultGlobalParamsIDs(GetVariableName(() => BackendDebugMessageShowToUser), "BackendDebugMessageShowToUser", 3, false, 30, false);



        public object Value { get; set; }
        public int Order { get; set; }
        public bool IsPrivate { get; set; }
        public DefaultGlobalParamsIDs(string _Code, string _Name, int _ID, object _Value, int _Order, bool _IsPrivate)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<DefaultGlobalParamsIDs>();
            Value = _Value;
            Order = _Order;
            IsPrivate = _IsPrivate;
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static DefaultGlobalParamsIDs GetByID(int _ID, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static DefaultGlobalParamsIDs GetByName(string _Name, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static DefaultGlobalParamsIDs GetByCode(string _Code, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
