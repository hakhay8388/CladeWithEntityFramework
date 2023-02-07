using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nMessageResultCommand
{
    public interface IMessageResultReceiver : ICommandReceiver
    {
        void ReceiveMessageResultData(cListenerEvent _ListenerEvent, IController _Controller, cMessageResultCommandData _ReceivedData);
    }
}
