using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand
{
    public interface IDataSource_CreateReceiver : ICommandReceiver
    {
        void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData);
    }
}
