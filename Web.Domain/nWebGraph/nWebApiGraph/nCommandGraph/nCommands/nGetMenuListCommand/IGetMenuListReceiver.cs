using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand
{
    public interface IGetMenuListReceiver : ICommandReceiver
    {
        void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData);
    }
}
