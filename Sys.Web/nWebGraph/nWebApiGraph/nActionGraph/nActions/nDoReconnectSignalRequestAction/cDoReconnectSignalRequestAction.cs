using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Sys.Web.Controllers;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoReconnectSignalRequestAction
{
	public class cDoReconnectSignalRequestAction : cBaseActionWithProps<cDoReconnectSignalRequestProps>, IActionWithProps<cDoReconnectSignalRequestProps>
	{

		public cDoReconnectSignalRequestAction(cApp _App, cBaseWebGraph _WebGraph)
		   : base(_App, _WebGraph, EActionType.DoReconnectSignalRequest)
		{
		}
	}
}
