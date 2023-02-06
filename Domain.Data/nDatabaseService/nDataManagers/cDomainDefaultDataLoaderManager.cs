using Base.Data.nDatabaseService;
using Base.Data.nDatabaseService.nDatabase;
using Base.FileData;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nAttributes;
using Domain.Data.nDatabaseService;
using Domain.Data.nDataService.nDataManagers.nLoaders;
using Sys.Data.nDatabaseService;
using Sys.Data.nDataService.nDataManagers;
using Sys.Data.nDataService.nDataManagers.nLoaders;

namespace Domain.Data.nDataService.nDataManagers
{
    [Register(typeof(IDomainDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDomainDefaultDataLoaderManager : cBaseDataManager, IDomainDefaultDataLoader
    {
        public cTestLoader TestLoader { get; set; }
        public cDomainDefaultDataLoaderManager(cDataServiceContext CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService
            , cTestLoader _TestLoader
            )
          : base(CoreServiceContext, _DataService, _FileDataService)
        {
            TestLoader = _TestLoader;
        }

        public void Load()
        {
            cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();

            __DatabaseContext.Perform(() => { TestLoader.Init(); });

        }
    }
}

