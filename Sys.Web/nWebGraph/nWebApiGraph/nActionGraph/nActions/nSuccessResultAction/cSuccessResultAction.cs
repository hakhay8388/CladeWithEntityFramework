using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Bootstrapper.Core.nApplication;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSuccessResultAction
{
    public class cSuccessResultAction : cBaseAction
    {

        public cSuccessResultAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.SuccessResult)
        {
        }

        public void Action(IController _Controller)
        {
            JObject __JsonObject = new JObject();
            Action(_Controller, __JsonObject, null, false);
        }
    }
}
