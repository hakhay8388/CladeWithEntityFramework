using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand
{
    public interface ILoginReceiver : ICommandReceiver
    {
        void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData);
    }
}
