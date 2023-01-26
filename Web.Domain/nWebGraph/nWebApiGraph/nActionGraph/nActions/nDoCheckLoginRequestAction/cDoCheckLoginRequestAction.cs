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

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction
{
    public class cDoCheckLoginRequestAction : cBaseActionWithProps<cDoCheckLoginRequestProps>, IActionWithProps<cDoCheckLoginRequestProps>
    {

        public cDoCheckLoginRequestAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.DoCheckLoginRequest)
        {
        }

		public void Action(IController _Controller)
		{
			base.Action(_Controller, new cDoCheckLoginRequestProps() { IsLogined = _Controller.ClientSession.IsLogined});
		}
	}
}
