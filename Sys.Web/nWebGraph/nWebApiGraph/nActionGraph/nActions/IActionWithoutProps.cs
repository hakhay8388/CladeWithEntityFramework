using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IActionWithoutProps : IAction
    {
        void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false);
    }
}
