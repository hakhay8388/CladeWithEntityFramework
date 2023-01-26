using Bootstrapper.Core.nApplication;
using Data.Domain.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;

namespace Web.Domain.nWebGraph.nComponentManager.nDataSourcesManager
{
    public interface IDataSource : IDataSource_ReadReceiver
        , IDataSource_GetSettingsReceiver
        , IDataSource_CreateReceiver
        , IDataSource_UpdateReceiver
        , IDataSource_DeleteReceiver
        , IDataSource_GetMetaDataReceiver
    {
        cApp App { get; set; }
        DataSourceIDs DataSourceID { get; set; }
        void Init();
        void SynchronizeColumnNames();
    

    }
}
