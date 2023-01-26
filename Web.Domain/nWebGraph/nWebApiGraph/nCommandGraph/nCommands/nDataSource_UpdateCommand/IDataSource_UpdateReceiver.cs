using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand
{
    public interface IDataSource_UpdateReceiver : ICommandReceiver
    {
        void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData);
    }
}
