using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using Bootstrapper.Core.nApplication;
using Web.Domain.Controllers;
using Sys.Data.nDatabaseService;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;

namespace Web.Domain.nWebGraph.nSessionManager
{
    public abstract class cBaseController : Controller, IController
    {
        public List<cSignalSessionMatcher> SignalSessions { get; set; }
        public cSession ClientSession { get; set; }
        public JObject CommandJson { get; set; }
        public JArray ActionJson { get; set; }

        public cWebGraph WebGraph { get; set; }
        public cApp App { get; set; }
        public string IpAddress { get; set; }
        public IDataService DataService { get; set; }
        public HttpContext CurrentContext { get; set; }
        public IHubContext<SignalRHub> SignalHub { get; set; }
        public bool IsSignal { get { return false; } }

        public cBaseController(cApp _App, cWebGraph _WebGraph, IDataService _DataService, IHubContext<SignalRHub> _SignalHub)
        {
            App = _App;
            WebGraph = _WebGraph;
            DataService = _DataService;
            SignalHub = _SignalHub;


        }
        public string GetWordValue(string _Word, params object[] _Parameters)
        {
            return App.Handlers.LanguageHandler.GetWordValue(ClientSession.Language, _Word, _Parameters);
        }
        static bool __BatchJobExecution = false;
        public override void OnActionExecuting(ActionExecutingContext _Context)
        {
            base.OnActionExecuting(_Context);
            CurrentContext = _Context.HttpContext;
            IpAddress = CurrentContext.Connection.RemoteIpAddress.ToString();
            var __Watch = new Stopwatch();
            __Watch.Start();

            CurrentContext.Response.OnStarting(_Props =>
            {
                string __Total = __Watch.ElapsedMilliseconds.ToString();
                CurrentContext.Response.Headers.Add("X-Response-Time-Milliseconds", __Total);

                return Task.CompletedTask;
            }, CurrentContext);


            InitializeClientSession(_Context);
            SignalSessions = new List<cSignalSessionMatcher>();
        }

        protected void InitializeClientSession(ActionExecutingContext _Context)
        {
            ActionJson = new JArray();
            //App.Handlers.ContextHandler.AddContext(_Context.HttpContext);
            ClientSession = WebGraph.SessionManager(CurrentContext).CreateSession(this);
        }

        public void Logout()
        {
            WebGraph.SessionManager(CurrentContext).Logout(this);
        }

        private void SendMessageToSessions(List<cSignalSessionMatcher> _SignalSessions)
        {
            _SignalSessions.ForEach(__Item =>
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
                                SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
                            }
                        }
                    });
                }
                else
                {
                    //if (__Item.Session.IsLogined)
                    //{
                        foreach (string __SignalRID in __Item.Session.SignalRIDList)
                        {
                            SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
                        }
                    //}
                }
            });
        }

        public void InstantSendSignalAll(JObject _Object)
        {
            List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
            cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
            __FindItem.ActionJson.Add(_Object);
            __SignalSessions.Add(__FindItem);
            SendMessageToSessions(__SignalSessions);
        }

        public void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object)
        {
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

                SendMessageToSessions(__SignalSessions);


            }
        }
        //burası düzelecek
        public void AddSignal(List<cSession> _Sessionlist, JObject _Object)
        {
            if (_Sessionlist != null)
            {
                if (_Sessionlist.Count > 0)
                {
                    _Sessionlist.ForEach(__Item =>
                    {
                        cSignalSessionMatcher __FindItem = SignalSessions.Find(__Main => __Main.Session != null && __Main.Session.SessionID == __Item.SessionID);
                        if (__FindItem == null)
                        {
                            __FindItem = new cSignalSessionMatcher(__Item);
                            SignalSessions.Add(__FindItem);
                        }
                        __FindItem.ActionJson.Add(_Object);
                    });
                }
                else
                {
                    cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
                    __FindItem.ActionJson.Add(_Object);
                    SignalSessions.Add(__FindItem);
                }
            }
        }
    }
}
