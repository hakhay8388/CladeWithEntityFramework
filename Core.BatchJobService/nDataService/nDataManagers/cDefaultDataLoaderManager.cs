using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDataService.nDataManagers.nLoaders;

using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDataService.nDataManagers;
using Core.BatchJobService.nDataService.nDataManagers.nLoaders;
using Sys.Data.nDataService;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Sys.Data.nDatabaseService;
using Base.Data.nDatabaseService;
using Base.FileData;
using Bootstrapper.Core.nApplication.nStarter;
using Domain.Data.nDatabaseService;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    [Register(typeof(IBatchJobDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IBatchJobDataLoader
    {
        public cBatchJobDataLoader BatchJobDataLoader { get; set; }
        public cBatchJobExecutionDataLoader BatchJobExecutionDataLoader { get; set; }

        public cDefaultDataLoaderManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService
            , cBatchJobDataLoader _BatchJobDataLoader
            , cBatchJobExecutionDataLoader _BatchJobExecutionDataLoader
        )
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
            BatchJobDataLoader = _BatchJobDataLoader;
            BatchJobExecutionDataLoader = _BatchJobExecutionDataLoader;
        }

        public void Load()
        {
            cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();

            __DatabaseContext.Perform(LoadBatchJobData);
            __DatabaseContext.Perform(LoadBatchJobExecutionData);
        }

        public void LoadBatchJobData()
        {
            ////////////////////////
            ///ÖNEMLİ
            ///Normalde pure data ve reletinal data olmasından dolayı önce pure dataları commitlemek sonra bağlantılı olanları eklemek gerekiyor.
            ///Fakat Aynı transaction üzerinden yönetildiğinde sorgular transaction üzerinde olanlarıda bulabiliyor.
            ///Onun için saf datalar ve bağlantılı datalar tek bir commitle gönderilebiliyor.
            BatchJobDataLoader.Init();
        }

        public void LoadBatchJobExecutionData()
        {
            ////////////////////////
            ///ÖNEMLİ
            ///Normalde pure data ve reletinal data olmasından dolayı önce pure dataları commitlemek sonra bağlantılı olanları eklemek gerekiyor.
            ///Fakat Aynı transaction üzerinden yönetildiğinde sorgular transaction üzerinde olanlarıda bulabiliyor.
            ///Onun için saf datalar ve bağlantılı datalar tek bir commitle gönderilebiliyor.
            BatchJobExecutionDataLoader.Init();
        }
    }
}

