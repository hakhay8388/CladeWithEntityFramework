

using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers;
using Data.Domain.nDefaultValueTypes;

namespace Web.Domain.nWebGraph.nComponentManager.nDataSourcesManager
{
    public abstract class cBaseListDataSourceForCustumQuery<TEntity> 
    {
        public cBaseListDataSourceForCustumQuery(
            DataSourceIDs _DataSourceID
            , cApp _App
            , cWebGraph _WebGraph
            , cDataService _DataService
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
        )
        {
        
        }
    }
}