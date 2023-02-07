using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand
{
    public interface IGetGlobalParamListReceiver : ICommandReceiver
    {
        void ReceiveGetGlobalParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetGlobalParamListCommandData _ReceivedData);
    }
}
