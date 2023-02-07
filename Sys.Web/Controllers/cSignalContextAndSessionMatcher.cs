using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.Controllers
{
    public class cSignalContextAndSessionMatcher
    {
        public HttpContext Context { get; set; }
        public List<cSignalSessionMatcher> SignalSessions { get; set; }
        public cSignalContextAndSessionMatcher(HttpContext HttpContext, List<cSignalSessionMatcher> _SignalSessions)
        {
            Context = HttpContext;
            SignalSessions = _SignalSessions;
        }
    }
}
