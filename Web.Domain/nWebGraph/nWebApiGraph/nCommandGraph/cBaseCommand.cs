using Newtonsoft.Json.Linq;
using Web.Domain.nUtils.nValueTypes;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Web.Domain.Controllers;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph
{
    public abstract class cBaseCommand : cCoreObject
    {
        public CommandIDs CommandID;
        public cWebGraph WebGraph { get; set; }

        public cBaseCommand(cApp _App, cWebGraph _WebGraph, CommandIDs _CommandID)
            : base(_App)
        {
            CommandID = _CommandID;
            WebGraph = _WebGraph;
        }

        public abstract void Interpret(IController _Controller, JToken _JsonObject);
    }

}
