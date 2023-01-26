using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;


namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLanguageAction
{
    public class cLanguageAction : cBaseActionWithProps<cLanguageProps>, IActionWithProps<cLanguageProps>
    {
        public cLanguageAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.Language)
        {
        }
    }
}
