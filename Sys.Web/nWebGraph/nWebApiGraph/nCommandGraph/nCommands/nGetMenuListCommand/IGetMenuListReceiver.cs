using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand
{
    public interface IGetMenuListReceiver : ICommandReceiver
    {
        void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData);
    }
}
