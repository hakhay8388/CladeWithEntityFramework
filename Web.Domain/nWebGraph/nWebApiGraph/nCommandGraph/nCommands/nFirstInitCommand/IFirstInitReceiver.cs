using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nFirstInitCommand
{
    public interface IFirstInitReceiver : ICommandReceiver
    {
        void ReceiveFirstInitData(cListenerEvent _ListenerEvent, IController _Controller, cFirstInitCommandData _ReceivedData);
    }
}
