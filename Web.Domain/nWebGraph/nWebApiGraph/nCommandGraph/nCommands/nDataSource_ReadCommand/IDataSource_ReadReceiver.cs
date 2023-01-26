using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand
{
    public interface IDataSource_ReadReceiver : ICommandReceiver
    {
        void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData);
    }
}
