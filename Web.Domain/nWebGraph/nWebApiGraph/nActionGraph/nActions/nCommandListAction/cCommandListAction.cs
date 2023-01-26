using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCommandListAction
{
    public class cCommandListAction : cBaseAction, IActionWithoutProps
    {
        public cCommandListAction(cApp _App, cWebGraph _WebGraph)
          : base(_App, _WebGraph, ActionIDs.CommandList)
        {
        }

        public override void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JObject __JsonObject = new JObject();
            __JsonObject["CommandList"] = JArray.FromObject(ActionIDs.TypeList);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

    }
}
