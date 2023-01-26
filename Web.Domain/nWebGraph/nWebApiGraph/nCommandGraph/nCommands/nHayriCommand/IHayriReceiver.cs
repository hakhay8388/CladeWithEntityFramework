using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nHayriCommand
{
    public interface IHayriReceiver : ICommandReceiver
    {
        void ReceiveHayriData(cListenerEvent _ListenerEvent, IController _Controller, cHayriCommandData _ReceivedData);
    }
}
