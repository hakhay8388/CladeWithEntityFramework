using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainDefaultGlobalParamsIDs : DefaultGlobalParamsIDs
    {
        public static DefaultGlobalParamsIDs TestParm1 { get; set; }

        public static void Init()
        {
            TestParm1 = new DefaultGlobalParamsIDs(GetVariableName(() => TestParm1), "TestParm1", 1, "test value", 1, false);
        }

        public DomainDefaultGlobalParamsIDs(string _Code, string _Name, int _ID, object _Value, int _Order, bool _IsPrivate)
            : base(_Code, _Name, _ID, _Value, _Order, _IsPrivate)
        {
        }
    }
}
