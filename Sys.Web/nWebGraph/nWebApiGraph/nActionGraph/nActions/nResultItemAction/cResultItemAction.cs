using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction
{
    public class cResultItemAction : cBaseActionWithProps<cResultItemProps>, IActionWithProps<cResultItemProps>
    {
        public cResultItemAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.ResultItem)
        {
        }
    }
}
