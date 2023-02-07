using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Domain.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nTestCommand;

namespace Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph.nGeneralListener
{
     public class cGeneralListener : cBaseListener
        , ITestReceiver
    {
        public cGeneralListener(cApp _App, cWebGraph _WebGraph, IDataService _DataService)
               : base(_App, _WebGraph, _DataService)
        {
        }

        public void ReceiveTestData(cListenerEvent _ListenerEvent, IController _Controller, cTestCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }
    }
}
