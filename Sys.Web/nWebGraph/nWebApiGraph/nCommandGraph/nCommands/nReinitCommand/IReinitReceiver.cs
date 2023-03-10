using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReinitCommand
{
    public interface IReinitReceiver : ICommandReceiver
    {
        void ReceiveReinitData(cListenerEvent _ListenerEvent, IController _Controller, cReinitCommandData _ReceivedData);
    }
}
