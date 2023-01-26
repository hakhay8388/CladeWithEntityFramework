using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDataService.nDataManagers.nLoaders;

using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDataService.nDataManagers;
using Core.BatchJobService.nDataService.nDataManagers.nLoaders;
using Data.Domain.nDataService;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Data.Domain.nDatabaseService;
using Base.Data.nDatabaseService;
using Base.FileData;
using Bootstrapper.Core.nApplication.nStarter;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    [Register(typeof(IBatchJobDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IBatchJobDataLoader
    {
        public cBatchJobDataLoader BatchJobDataLoader { get; set; }
        public cBatchJobExecutionDataLoader BatchJobExecutionDataLoader { get; set; }

        public cDefaultDataLoaderManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService
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
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

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

