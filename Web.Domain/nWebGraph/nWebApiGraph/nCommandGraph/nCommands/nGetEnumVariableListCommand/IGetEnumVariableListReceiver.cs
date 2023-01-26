using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetEnumVariableListCommand
{
    public interface IGetEnumVariableListReceiver : ICommandReceiver
    {
        void ReceiveGetEnumVariableListData(cListenerEvent _ListenerEvent, IController _Controller, cGetEnumVariableListCommandData _ReceivedData);
    }
}
