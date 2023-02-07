using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph;
using Sys.Data.nDatabaseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sys.Web.nWebGraph;
using Sys.Web.nWebGraph.nSessionManager;

using Base.Data.nDatabaseService;

namespace Sys.Web.Controllers
{
	public class SignalRHub : Hub, IController
	{
		public static List<IGlobalSignalReceiver> SignalReceiverList = new List<IGlobalSignalReceiver>();

		public static void ConnectToSignalRHub(IGlobalSignalReceiver _Receiver)
		{
			SignalReceiverList.Add(_Receiver);
		}

		public static void DisconnectToSignalRHub(IGlobalSignalReceiver _Receiver)
		{
			SignalReceiverList.Remove(_Receiver);
		}

		public bool IsSignal { get{return true;}}
		public cApp App { get; set; }
		public cBaseWebGraph WebGraph { get; set; }
		public cSession ClientSession { get; set; }
		public JObject CommandJson { get; set; }
		public JArray ActionJson { get; set; }
		public IDataService DataService { get; set; }
		public HttpContext CurrentContext { get; set; }

		public IHubContext<SignalRHub> SignalHub { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public List<cSignalSessionMatcher> SignalSessions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public List<cSignalContextAndSessionMatcher> SignalContexts { get; set; }

		public SignalRHub()
			: base()
		{
			App = cApp.App;
			WebGraph = App.Factories.ObjectFactory.ResolveInstance<cBaseWebGraph>();
			SignalContexts = new List<cSignalContextAndSessionMatcher>();
		}


		public void BroadCastData(cSession _Session)
		{
			List<cSignalContextAndSessionMatcher> __SignalContextItems = SignalContexts.Where(__Item => __Item.Context == CurrentContext).ToList();
			for (int i = 0; i < __SignalContextItems.Count; i++)
			{
				SignalContexts.Remove(__SignalContextItems[i]);
				__SignalContextItems[i].SignalSessions.ForEach(__Item =>
				{
					if (__Item.Session == null)
					{
						WebGraph.SessionManager(CurrentContext).GetSessionList().ForEach(__SessionItems =>
						{
							//SignalHub.Clients.All.SendAsync("CommandChannel", __Item.ActionJson.ToString());
							if (__SessionItems.IsLogined)
							{
								foreach (string __SignalRID in __SessionItems.SignalRIDList)
								{
									Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
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
								Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
							}
						}
					}
				});

			}
		}

		public override	 Task OnConnectedAsync()
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);

			lock (ClientSession.SignalRIDList)
			{
				if (ClientSession.SignalRIDList.Find(__Item => __Item == this.Context.ConnectionId) == null)
				{
					ClientSession.SignalRIDList.Add(this.Context.ConnectionId);
				}
			}

			SignalReceiverList.ForEach(__Item => __Item.OnConnected(this));

			BroadCastData(ClientSession);

			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);
			lock (ClientSession.SignalRIDList)
			{
				ClientSession.SignalRIDList.RemoveAll(__Item => __Item == this.Context.ConnectionId);
			}

			SignalReceiverList.ForEach(__Item => __Item.OnDisconnected(this));

			BroadCastData(ClientSession);

			return base.OnDisconnectedAsync(exception);
		}


		public async Task SendMessage(JArray _JArray)
		{
			await Clients.All.SendAsync("CommandChannel", _JArray.ToString());
		}

		public async Task ControlConnection(string _Message)
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			string __SessionID = CurrentContext.Request.Cookies[cSessionManager.CookieSessionName];
			cSession __Session = WebGraph.SessionManager(CurrentContext).GetSessionByID(__SessionID);
			if (__Session == null)
			{
				__Session = WebGraph.SessionManager(CurrentContext).CreateSession(this);
			}

			if (__Session.SignalRIDList.Find(__Item => __Item == this.Context.ConnectionId) == null)
			{
				__Session.SignalRIDList.Add(this.Context.ConnectionId);
			}

			/*if (_Message == "Logined=true" && !__Session.IsLogined)
			{
				await Clients.Caller.SendAsync("ControlConnection", "Reconnect=true");
			}
			else if (_Message == "Logined=false" && __Session.IsLogined)
			{
				await Clients.Caller.SendAsync("ControlConnection", "Reconnect=true");
			}
			else
			{
				await Clients.Caller.SendAsync("ControlConnection", "true");
			}*/

			await Clients.Caller.SendAsync("ControlConnection", _Message);


		}

		public async Task ActionConnection(string _Message)
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);

			lock (ClientSession.SignalRIDList)
			{
				if (ClientSession.SignalRIDList.Find(__Item => __Item == this.Context.ConnectionId) == null)
				{
					ClientSession.SignalRIDList.Add(this.Context.ConnectionId);
				}
			}


			CommandJson = JObject.Parse(_Message);
			WebGraph.CommandGraph.InterpreterCommand(this);
		}



		public void Logout()
		{
			//throw new NotImplementedException();
		}

		private void SendMessageToSessions(List<cSignalContextAndSessionMatcher> _SignalContexts)
		{
			List<cSignalContextAndSessionMatcher> __SignalContextItems = _SignalContexts.Where(__Item => __Item.Context == CurrentContext).ToList();
			for (int i = 0; i < __SignalContextItems.Count; i++)
			{
				_SignalContexts.Remove(__SignalContextItems[i]);
				__SignalContextItems[i].SignalSessions.ForEach(__Item =>
				{
					if (__Item.Session == null)
					{
						WebGraph.SessionManager(CurrentContext).GetSessionList().ForEach(__SessionItems =>
						{
							//SignalHub.Clients.All.SendAsync("CommandChannel", __Item.ActionJson.ToString());
							if (__SessionItems.IsLogined)
							{
								foreach (string __SignalRID in __SessionItems.SignalRIDList)
								{
									Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
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
								Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
							}
						}
					}
				});

			}
		}

		public void InstantSendSignalAll(JObject _Object)
		{
			List<cSignalContextAndSessionMatcher> __SignalContexts = new List<cSignalContextAndSessionMatcher>();
			cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
			__FindItem.ActionJson.Add(_Object);
			__SignalContexts.Add(new cSignalContextAndSessionMatcher(CurrentContext, new List<cSignalSessionMatcher>() { __FindItem }));
			SendMessageToSessions(__SignalContexts);
		}

		public void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object)
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);


			if (_Sessionlist != null)
			{
				List<cSignalContextAndSessionMatcher> __SignalContexts = new List<cSignalContextAndSessionMatcher>();

				if (_Sessionlist.Count > 0)
				{
					List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
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

					__SignalContexts.Add(new cSignalContextAndSessionMatcher(CurrentContext, __SignalSessions));
				}
				SendMessageToSessions(__SignalContexts);
			}
		}

		public void AddSignal(List<cSession> _Sessionlist, JObject _Object)
		{
			CurrentContext = Context.GetHttpContext();
			//App.Handlers.ContextHandler.AddContext(Context.GetHttpContext());

			ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);

			if (_Sessionlist != null)
			{
				if (_Sessionlist.Count > 0)
				{
					List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
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

					SignalContexts.Add(new cSignalContextAndSessionMatcher(CurrentContext, __SignalSessions));
				}
				else
				{
					cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
					__FindItem.ActionJson.Add(_Object);
					SignalContexts.Add(new cSignalContextAndSessionMatcher(CurrentContext, new List<cSignalSessionMatcher>() { __FindItem }));
				}
			}
		}

		public string GetWordValue(string _Word, params object[] _Parameters)
		{
			return App.Handlers.LanguageHandler.GetWordValue(ClientSession.Language, _Word, _Parameters);
		}

		/*        public void SendAll(JArray _JArray)
                {
                    Clients.All.SendAsync("CommandChannel", _JArray.ToString());
                }

                public void SendTo(cSession _Session, JArray _JArray)
                {
                    Clients.Client(_Session.SignalRID).SendAsync("CommandChannel", _JArray.ToString());
                }

                public void CommandChannel(string _Command)
                {
                    Clients.All.SendAsync("CommandChannel", _Command);
                }

                public void Logout()
                {
                    WebGraph.SessionManager.Logout(this);
                }

                public void AddSignal(List<cSession> _Sessionlist, JObject _Object)
                {
                    throw new NotImplementedException();
                }*/
	}
}
