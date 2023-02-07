using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph;

namespace Sys.Web.Controllers
{
    public interface IController
    {
        List<cSignalSessionMatcher> SignalSessions { get; set; }
        HttpContext CurrentContext { get; set; }
        IHubContext<SignalRHub> SignalHub { get; set; }
        cSession ClientSession { get; set; }
        JObject CommandJson { get; set; }
        JArray ActionJson { get; set; }
        cBaseWebGraph WebGraph { get; set; }
        cApp App { get; set; }
        IDataService DataService { get; set; }
        string GetWordValue(string _Word, params object[] _Parameters);
        void Logout();
        void AddSignal(List<cSession> _Sessionlist, JObject _Object);
        void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object);

		void InstantSendSignalAll(JObject _Object);

		bool IsSignal { get; }
	}
}
