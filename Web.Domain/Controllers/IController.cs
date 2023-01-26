using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph;
using Data.Domain.nDatabaseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nSessionManager;

namespace Web.Domain.Controllers
{
    public interface IController
    {
        List<cSignalSessionMatcher> SignalSessions { get; set; }
        HttpContext CurrentContext { get; set; }
        IHubContext<SignalRHub> SignalHub { get; set; }
        cSession ClientSession { get; set; }
        JObject CommandJson { get; set; }
        JArray ActionJson { get; set; }
        cWebGraph WebGraph { get; set; }
        cApp App { get; set; }
        cDataService DataService { get; set; }
        string GetWordValue(string _Word, params object[] _Parameters);
        void Logout();
        void AddSignal(List<cSession> _Sessionlist, JObject _Object);
        void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object);

		void InstantSendSignalAll(JObject _Object);

		bool IsSignal { get; }
	}
}
