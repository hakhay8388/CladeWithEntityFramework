using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;


namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction
{
    public class cValidationResultAction : cBaseActionWithProps<cValidationResultProps>, IActionWithProps<cValidationResultProps>
    {

        public cValidationResultAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ValidationResult)
        {
        }
    }
}
