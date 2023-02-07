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

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction
{
    public class cDoCheckLoginRequestAction : cBaseActionWithProps<cDoCheckLoginRequestProps>, IActionWithProps<cDoCheckLoginRequestProps>
    {

        public cDoCheckLoginRequestAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.DoCheckLoginRequest)
        {
        }

		public void Action(IController _Controller)
		{
			base.Action(_Controller, new cDoCheckLoginRequestProps() { IsLogined = _Controller.ClientSession.IsLogined});
		}
	}
}
