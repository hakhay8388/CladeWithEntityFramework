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
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction
{
	public class cReinitAction : cBaseActionWithProps<cReinitProps>, IActionWithProps<cReinitProps>
	{
		public cReinitAction(cApp _App, cBaseWebGraph _WebGraph)
		   : base(_App, _WebGraph, EActionType.Reinit)
		{
		}

		public override void Action(IController _Controller = null, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{

            cReinitProps __LogInOutProps = new cReinitProps();
			__LogInOutProps.LoginState = _Controller.ClientSession.IsLogined;
			__LogInOutProps.SessionID = _Controller.ClientSession.SessionID;

			JObject __JsonObject = __LogInOutProps.SerializeObject();

			base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
		}

	}
}
