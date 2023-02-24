using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public interface ILookupDataSource
    {
        object ToLookUpObject(cListenerEvent _ListenerEvent, IController _Controller);
    }
}
