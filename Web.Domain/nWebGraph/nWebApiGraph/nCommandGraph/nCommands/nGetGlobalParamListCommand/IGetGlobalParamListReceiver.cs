using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand
{
    public interface IGetGlobalParamListReceiver : ICommandReceiver
    {
        void ReceiveGetGlobalParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetGlobalParamListCommandData _ReceivedData);
    }
}
