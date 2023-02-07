using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCommandListAction
{
    public class cCommandListAction : cBaseAction, IActionWithoutProps
    {
        public cCommandListAction(cApp _App, cBaseWebGraph _WebGraph)
          : base(_App, _WebGraph, EActionType.CommandList)
        {
        }

        public override void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JObject __JsonObject = new JObject();
            __JsonObject["CommandList"] = JArray.FromObject(EActionType.TypeList);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

    }
}
