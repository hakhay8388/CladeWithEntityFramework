using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;

using Web.Domain.Controllers;
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction
{
	public class cLogInOutAction : cBaseActionWithProps<cLogInOutProps>, IActionWithProps<cLogInOutProps>
	{
		public cLogInOutAction(cApp _App, cWebGraph _WebGraph)
		   : base(_App, _WebGraph, ActionIDs.LogInOut)
		{
		}

		public override void Action(IController _Controller = null, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			if (_Controller.ClientSession.IsLogined)
			{
				_Controller.ClientSession.User.Load(__Item => __Item.Roles);
				_Controller.ClientSession.User.Load(__Item => __Item.UserDetail);
			}


            cLogInOutProps __LogInOutProps = new cLogInOutProps();
			__LogInOutProps.LoginState = _Controller.ClientSession.IsLogined;
			__LogInOutProps.SessionID = _Controller.ClientSession.SessionID;



            __LogInOutProps.User = _Controller.ClientSession.User != null ? _Controller.ClientSession.User : null;

			JObject __JsonObject = __LogInOutProps.SerializeObject();

			if (__JsonObject["User"].HasValues)
			{
				__JsonObject["User"]["Password"] = null;
			}

			base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
		}

	}
}
