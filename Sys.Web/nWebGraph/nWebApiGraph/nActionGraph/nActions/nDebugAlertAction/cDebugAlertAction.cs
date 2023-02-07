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

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction
{
    public class cDebugAlertAction : cBaseActionWithProps<cDebugAlertProps>, IActionWithProps<cDebugAlertProps>
    {

        public cDebugAlertAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.DebugAlert)
        {
        }

		public void ErrorAction(Exception _Ex, IController _Controller, cDebugAlertProps _DebugAlertProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			App.Loggers.CoreLogger.LogError(_Ex);
			base.Action(_Controller, _DebugAlertProps.SerializeObject(), _SignalSessions, _InstantSend);
		}

		public void ErrorAction(IController _Controller, cDebugAlertProps _DebugAlertProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			base.Action(_Controller, _DebugAlertProps.SerializeObject(), _SignalSessions, _InstantSend);
		}
	}
}
