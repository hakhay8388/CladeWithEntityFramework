using Newtonsoft.Json.Linq;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Sys.Web.Controllers;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph
{
    public abstract class cBaseCommand : cCoreObject
    {
        public ECommandType CommandID;
        public cBaseWebGraph WebGraph { get; set; }

        public cBaseCommand(cApp _App, cBaseWebGraph _WebGraph, ECommandType _CommandID)
            : base(_App)
        {
            CommandID = _CommandID;
            WebGraph = _WebGraph;
        }

        public abstract void Interpret(IController _Controller, JToken _JsonObject);
    }

}
