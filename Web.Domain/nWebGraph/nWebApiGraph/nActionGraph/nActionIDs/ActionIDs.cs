using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs
{
    public class ActionIDs
    {
        public static EActionType Test = new EActionType(nameof(Test), 1000, "", true);

    }
}
