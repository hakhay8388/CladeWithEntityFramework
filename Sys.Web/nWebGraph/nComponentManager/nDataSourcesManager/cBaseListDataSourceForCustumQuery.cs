

using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager;
using Sys.Data.nDatabaseService;
using Sys.Data.nDataService.nDataManagers;
using Sys.Boundary.nDefaultValueTypes;
using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager
{
    public abstract class cBaseListDataSourceForCustumQuery<TEntity> 
    {
        public cBaseListDataSourceForCustumQuery(
            DataSourceIDs _DataSourceID
            , cApp _App
            , cBaseWebGraph _WebGraph
            , IDataService _DataService
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
        )
        {
        
        }
    }
}