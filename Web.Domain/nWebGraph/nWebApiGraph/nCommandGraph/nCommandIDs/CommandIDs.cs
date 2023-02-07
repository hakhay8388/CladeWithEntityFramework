using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs
{
    public class CommandIDs
    {
        public static ECommandType Test { get; private set; }


        public static void Init()
        {
            Test = new ECommandType(nameof(Test), 1000, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        }
    }
}
