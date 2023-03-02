using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

          
namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nAddLawsuitCommand
{
    public interface IAddLawsuitReceiver : ICommandReceiver
    {
        void ReceiveAddLawsuitData(cListenerEvent _ListenerEvent, IController _Controller, cAddLawsuitCommandData _ReceivedData);
    }
}
