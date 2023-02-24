using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Bootstrapper.Core.nCore;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph
{
    public class cBaseListener : cCoreObject, IBaseListener
    {
        public cBaseWebGraph WebGraph { get; set; }

        public IDataService DataService { get; set; }

        public Dictionary<Type, int> ListenerOrders { get; set; }

		protected IHubContext<SignalRHub> SignalHub { get; set; }


		public cBaseListener(cApp _App,  cBaseWebGraph _WebGraph, IDataService _DataService, Dictionary<Type, int> _ListenerOrders = null)
			: base(_App)
        {
            if (_ListenerOrders != null)
            {
                ListenerOrders = _ListenerOrders;
            }
            else
            {
                ListenerOrders = new Dictionary<Type, int>();
            }
            
            WebGraph = _WebGraph;
            DataService = _DataService;
        }

        public override void Init()
        {
			WebGraph.CommandGraph.ConnectToCommands(this); 
        }


		private void SendMessageToSessions(IController _Controller, List<cSignalSessionMatcher> _SignalSessions)
		{
			_SignalSessions.ForEach(__Item =>
			{
				if (__Item.Session == null)
				{
					WebGraph.SessionManager(_Controller).GetSessionList().ForEach(__SessionItems =>
					{
						//SignalHub.Clients.All.SendAsync("CommandChannel", __Item.ActionJson.ToString());
						if (__SessionItems.IsLogined)
						{
							foreach (string __SignalRID in __SessionItems.SignalRIDList)
							{
								SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
							}
						}
					});
				}
				else
				{
					if (__Item.Session.IsLogined)
					{
						foreach (string __SignalRID in __Item.Session.SignalRIDList)
						{
							SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
						}
					}
				}
			});
		}

	
		public void InstantSendSignal(IController _Controller, List<cSession> _Sessionlist, JObject _Object)
		{
			if (SignalHub == null)
			{
				SignalHub = App.Factories.ObjectFactory.ResolveInstance<IHubContext<SignalRHub>>();
			}			
			if (_Sessionlist != null)
			{
				List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
				if (_Sessionlist.Count > 0)
				{
					_Sessionlist.ForEach(__Item =>
					{
						cSignalSessionMatcher __FindItem = __SignalSessions.Find(__Main => __Main.Session.SessionID == __Item.SessionID);
						if (__FindItem == null)
						{
							__FindItem = new cSignalSessionMatcher(__Item);
							__SignalSessions.Add(__FindItem);
						}
						__FindItem.ActionJson.Add(_Object);
					});
				}

				SendMessageToSessions(_Controller, __SignalSessions);
			}
		}


	}
}
