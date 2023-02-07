using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction
{
    public class cNotificationAction : cBaseActionWithProps<cNotificationProps>
    {

        public cNotificationAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.Notification)
        {
        }
    }
}
