using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction
{
    public class cResultItemAction : cBaseActionWithProps<cResultItemProps>, IActionWithProps<cResultItemProps>
    {
        public cResultItemAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ResultItem)
        {
        }
    }
}
