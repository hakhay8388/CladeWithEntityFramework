using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nAsyncLoadAction
{
    public class cAsyncLoadAction : cBaseActionWithProps<cAsyncLoadProps>
    {

        public cAsyncLoadAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.AsyncLoad)
        {
        }
    }
}
