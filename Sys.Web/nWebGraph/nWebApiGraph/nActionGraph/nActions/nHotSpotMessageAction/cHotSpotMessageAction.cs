using Newtonsoft.Json.Linq;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction
{
    public class cHotSpotMessageAction : cBaseActionWithProps<cHotSpotProps>, IActionWithProps<cHotSpotProps>
    {

        public cHotSpotMessageAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.HotSpotMessage)
        {
        }

      
    }
}
