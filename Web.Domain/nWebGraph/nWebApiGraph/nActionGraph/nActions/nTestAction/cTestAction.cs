using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nTestAction
{
    public class cTestAction : cBaseActionWithProps<cTestProps>, IActionWithProps<cTestProps>
    {

        public cTestAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.Test)
        {
        }
    }
}
