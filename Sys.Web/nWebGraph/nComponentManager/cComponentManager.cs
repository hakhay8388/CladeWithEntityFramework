using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Data.nDatabaseService;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication;
using Base.FileData;
using Sys.Data.nDatabaseService;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager;


namespace Sys.Web.nWebGraph.nComponentManager
{
    [Register(typeof(IComponentLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cComponentManager : cCoreObject, IComponentLoader
    {
        public cDataSourceManager DataSourceManager { get; set; }

        public cDataServiceContext CoreServiceContext { get; set; }
        public IDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }

        public cComponentManager(cApp _App, cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService
            , cDataSourceManager _DataSourceManager
            )
            : base(_App)
        {
            CoreServiceContext = _CoreServiceContext;
            DataService = _DataService;
            FileDataService = _FileDataService;
            DataSourceManager = _DataSourceManager;
        }

        public override void Init()
        {
             DataSourceManager = App.Factories.ObjectFactory.ResolveInstance<cDataSourceManager>();


            DataSourceManager.Init();
        }

        public void Load()
        {
            cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
            __DatabaseContext.Perform(LoadDefaultPureData);
        }
        public void LoadDefaultPureData()
        {
            DataSourceManager.FirtsRequestInit(); 
        }
    }
}
