using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReadNotificationCommand
{
    public interface IReadNotificationReceiver : ICommandReceiver
    {
        void ReceiveReadNotificationData(cListenerEvent _ListenerEvent, IController _Controller, cReadNotificationCommandData _ReceivedData);
    }
}
