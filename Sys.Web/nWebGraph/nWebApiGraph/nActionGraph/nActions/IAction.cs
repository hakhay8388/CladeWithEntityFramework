using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IAction
    {
        EActionType ActionID { get; set; }
        cBaseWebGraph WebGraph { get; set; }
    }
}
