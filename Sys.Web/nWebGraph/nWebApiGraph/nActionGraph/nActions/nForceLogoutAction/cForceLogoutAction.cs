using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;

using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nForceLogoutAction
{
	public class cForceLogoutAction : cBaseActionWithProps<cForceLogoutProps>, IActionWithProps<cForceLogoutProps>
	{

		public cForceLogoutAction(cApp _App, cBaseWebGraph _WebGraph)
		   : base(_App, _WebGraph, EActionType.ForceLogout)
		{
		}
 

	}
}
