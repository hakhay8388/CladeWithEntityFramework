using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetServerDateTimeCommand
{
    public interface IGetServerDateTimeReceiver : ICommandReceiver
    {
        void ReceiveGetServerDateTimeData(cListenerEvent _ListenerEvent, IController _Controller, cGetServerDateTimeCommandData _ReceivedData);
    }
}
