using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseAction: cCoreObject, IAction
    {
        public ActionIDs ActionID { get; set; }
        public cWebGraph WebGraph { get; set; }

        public cBaseAction(cApp _App, cWebGraph _WebGraph, ActionIDs _ActionID)
            :base(_App)
        {
            ActionID = _ActionID;
            WebGraph = _WebGraph;
            WebGraph.ActionGraph.ActionList.Add(this);
            WebGraph.CommandGraph.ConnectToCommands(this);
        }

		protected virtual void ActionAll(IController _Controller, JObject _Object)
		{
			_Controller.InstantSendSignalAll(PrepereObject(_Object));
		}

		protected virtual void InstantAction(IController _Controller, cBaseListener _Listener, JObject _Object, List<cSession> _SignalSessions)
		{
			_Listener.InstantSendSignal(_Controller, _SignalSessions, PrepereObject(_Object));
		}


		protected virtual void Action(IController _Controller, JObject _Object, List<cSession> _SignalSessions, bool _InstantSend)
        {
            if (_InstantSend && _SignalSessions != null)
            {
                _Controller.InstantSendSignal(_SignalSessions, PrepereObject(_Object));
            }
            else
            {
				if (_Controller.IsSignal)
				{
					_Controller.AddSignal(new List<cSession>() { _Controller.ClientSession}, PrepereObject(_Object));
				}
				else
				{
					if (_SignalSessions == null)
					{
						AddAction(_Controller.ActionJson, _Object);
					}
					else
					{
						_Controller.AddSignal(_SignalSessions, PrepereObject(_Object));
					}
				}
            }            
        }
        private JObject PrepereObject(JObject _Object)
        {
            JObject __JsonObject = new JObject();
            __JsonObject["ActionID"] = JObject.FromObject(ActionID);
            __JsonObject["Data"] = _Object;
            return __JsonObject;
        }

        private void AddAction(JArray _ActionArray, JObject _Object)
        {
            _ActionArray.Add(PrepereObject(_Object));
        }
        
        public virtual void Action(IController _Controller, List<cSession> _SignalSessions, bool _InstantSend = false)
        {
            Action(_Controller, JObject.FromObject(new { }), _SignalSessions, _InstantSend);
        }

    }
}
