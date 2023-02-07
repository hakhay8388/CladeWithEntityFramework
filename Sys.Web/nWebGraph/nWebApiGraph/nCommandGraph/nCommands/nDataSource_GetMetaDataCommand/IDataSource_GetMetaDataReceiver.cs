using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand
{
    public interface IDataSource_GetMetaDataReceiver : ICommandReceiver
    {
        void ReceiveDataSource_GetMetaDataData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData);
    }
}
