
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand
{
    public interface IGetPageListReceiver : ICommandReceiver
    {
        void ReceiveGetPageListData(cListenerEvent _ListenerEvent, IController _Controller, cGetPageListCommandData _ReceivedData);
    }
}
