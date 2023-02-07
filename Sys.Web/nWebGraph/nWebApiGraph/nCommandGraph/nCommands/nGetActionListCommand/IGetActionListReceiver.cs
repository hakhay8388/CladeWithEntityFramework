using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetActionListCommand
{
    public interface IGetActionListReceiver : ICommandReceiver
    {
        void ReceiveGetActionListData(cListenerEvent _ListenerEvent, IController _Controller, cGetActionListCommandData _ReceivedData);
    }
}
