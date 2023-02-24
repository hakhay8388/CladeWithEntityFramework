using Bootstrapper.Core.nApplication;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaAndSettingsCommand;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager
{
    public interface IDataSource : IBaseListener
        , IDataSource_ReadReceiver
        , IDataSource_CreateReceiver
        , IDataSource_UpdateReceiver
        , IDataSource_DeleteReceiver
        , IDataSource_GetMetaAndSettingsReceiver
    {
        cApp App { get; set; }
        DataSourceIDs DataSourceID { get; set; }
        void Init();
        void SynchronizeColumnNames();
    }
}
