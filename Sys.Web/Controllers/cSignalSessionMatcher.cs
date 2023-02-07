using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nSessionManager;

namespace Sys.Web.Controllers
{
    public class cSignalSessionMatcher
    {
        public cSession Session { get; set; }
        public JArray ActionJson { get; set; }
        public cSignalSessionMatcher(cSession _Session)
        {
            Session = _Session;
            ActionJson = new JArray();
        }
    }
}
