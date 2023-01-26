
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand
{
    public interface IGetPageListReceiver : ICommandReceiver
    {
        void ReceiveGetPageListData(cListenerEvent _ListenerEvent, IController _Controller, cGetPageListCommandData _ReceivedData);
    }
}
