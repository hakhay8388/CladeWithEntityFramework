using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Bootstrapper.Core.nApplication;
using Web.Domain.Controllers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSuccessResultAction
{
    public class cSuccessResultAction : cBaseAction
    {

        public cSuccessResultAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.SuccessResult)
        {
        }

        public void Action(IController _Controller)
        {
            JObject __JsonObject = new JObject();
            Action(_Controller, __JsonObject, null, false);
        }
    }
}
