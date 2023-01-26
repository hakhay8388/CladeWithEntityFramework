using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.Controllers;
using System.Collections.Generic;
using Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseActionWithProps<TActionProps> : 
        cBaseAction, 
        IActionWithProps<TActionProps>
         where TActionProps : IActionProps
    {
        public cBaseActionWithProps(cApp _App, cWebGraph _WebGraph, ActionIDs _ActionID)
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
