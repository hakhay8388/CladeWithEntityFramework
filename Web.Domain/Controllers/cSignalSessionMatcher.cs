using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nSessionManager;

namespace Web.Domain.Controllers
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
