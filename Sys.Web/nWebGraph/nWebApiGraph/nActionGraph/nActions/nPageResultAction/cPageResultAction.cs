using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;


namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction
{
    public class cPageResultAction : cBaseActionWithProps<cPageResultProps>, IActionWithProps<cPageResultProps>
    {

        public cPageResultAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.PageResult)
        {
        }

    }
}
