using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Sys.Web.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseAction: cCoreObject, IAction
    {
        public EActionType ActionID { get; set; }
        public cBaseWebGraph WebGraph { get; set; }

        public cBaseAction(cApp _App, cBaseWebGraph _WebGraph, EActionType _ActionID)
            :base(_App)
        {
            ActionID = _ActionID;
            WebGraph = _WebGraph;
            WebGraph.SysActionGraph.ActionList.Add(this);
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
