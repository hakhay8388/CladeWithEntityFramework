using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaAndSettingsCommand
{
    public interface IDataSource_GetMetaAndSettingsReceiver : ICommandReceiver
    {
        void ReceiveDataSource_GetMetaAndSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaAndSettingsCommandData _ReceivedData);
    }
}
