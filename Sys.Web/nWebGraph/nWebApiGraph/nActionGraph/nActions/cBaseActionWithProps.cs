using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Sys.Web.Controllers;
using System.Collections.Generic;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseActionWithProps<TActionProps> : 
        cBaseAction, 
        IActionWithProps<TActionProps>
         where TActionProps : IActionProps
    {
        public cBaseActionWithProps(cApp _App, cBaseWebGraph _WebGraph, EActionType _ActionID)
            :base(_App, _WebGraph, _ActionID)
        {            
        }
        
        public virtual void Action(IController _Controller, TActionProps _ActionData, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            Action(_Controller, _ActionData.SerializeObject(), _SignalSessions, _InstantSend);
        }

		public virtual void ActionAll(IController _Controller, TActionProps _ActionData)
		{
			ActionAll(_Controller, _ActionData.SerializeObject());
		}

		public virtual void Action(IController _Controller, cBaseListener _Listener, TActionProps _ActionData, List<cSession> _SignalSessions)
		{
			InstantAction(_Controller, _Listener, _ActionData.SerializeObject(), _SignalSessions);
		}

	}
}
