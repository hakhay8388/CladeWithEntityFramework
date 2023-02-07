using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

          
namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nTestCommand
{
    public interface ITestReceiver : ICommandReceiver
    {
        void ReceiveTestData(cListenerEvent _ListenerEvent, IController _Controller, cTestCommandData _ReceivedData);
    }
}
