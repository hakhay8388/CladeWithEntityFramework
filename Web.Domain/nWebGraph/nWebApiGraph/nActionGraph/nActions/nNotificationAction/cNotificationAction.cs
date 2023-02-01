using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction
{
    public class cNotificationAction : cBaseActionWithProps<cNotificationProps>
    {

        public cNotificationAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.Notification)
        {
        }
    }
}
