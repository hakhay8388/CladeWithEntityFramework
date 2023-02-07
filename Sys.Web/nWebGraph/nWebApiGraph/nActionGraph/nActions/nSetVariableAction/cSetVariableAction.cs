using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetVariableAction
{
    public class cSetVariableAction : cBaseActionWithProps<cVariableProps>
    {

        public cSetVariableAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.SetVariable)
        {
        }
    }
}
