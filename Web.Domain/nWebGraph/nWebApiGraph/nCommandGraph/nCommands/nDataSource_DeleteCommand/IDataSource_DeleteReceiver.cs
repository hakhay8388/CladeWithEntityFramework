using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand
{
    public interface IDataSource_DeleteReceiver : ICommandReceiver
    {
        void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData);
    }
}
