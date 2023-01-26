using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand
{
    public interface ILogoutReceiver : ICommandReceiver
    {
        void ReceiveLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cLogoutCommandData _ReceivedData);
    }
}
