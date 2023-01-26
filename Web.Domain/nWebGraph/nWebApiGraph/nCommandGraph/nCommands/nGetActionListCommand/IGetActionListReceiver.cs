using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetActionListCommand
{
    public interface IGetActionListReceiver : ICommandReceiver
    {
        void ReceiveGetActionListData(cListenerEvent _ListenerEvent, IController _Controller, cGetActionListCommandData _ReceivedData);
    }
}
