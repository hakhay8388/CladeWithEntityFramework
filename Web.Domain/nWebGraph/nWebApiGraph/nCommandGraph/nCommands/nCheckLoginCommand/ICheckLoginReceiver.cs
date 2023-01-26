using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand
{
    public interface ICheckLoginReceiver : ICommandReceiver
    {
        void ReceiveCheckLoginData(cListenerEvent _ListenerEvent, IController _Controller, cCheckLoginCommandData _ReceivedData);
    }
}
