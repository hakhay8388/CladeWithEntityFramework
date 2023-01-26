using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Web.Domain.Controllers;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoReconnectSignalRequestAction
{
	public class cDoReconnectSignalRequestAction : cBaseActionWithProps<cDoReconnectSignalRequestProps>, IActionWithProps<cDoReconnectSignalRequestProps>
	{

		public cDoReconnectSignalRequestAction(cApp _App, cWebGraph _WebGraph)
		   : base(_App, _WebGraph, ActionIDs.DoReconnectSignalRequest)
		{
		}
	}
}
