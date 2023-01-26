using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IActionWithoutProps : IAction
    {
        void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false);
    }
}
