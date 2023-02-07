
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetNotificationsCommand
{
    public interface IGetNotificationsReceiver : ICommandReceiver
    {
        void ReceiveGetNotificationsData(cListenerEvent _ListenerEvent, IController _Controller, cGetNotificationsCommandData _ReceivedData);
    }
}
