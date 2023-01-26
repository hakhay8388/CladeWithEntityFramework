using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDataService.nDataManagers;
using Data.Boundary.nData;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDataService;
using Core.BatchJobService.nDefaultValueTypes;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBatchJobDataLoader : cBaseDataLoader
    {
        public cBatchJobDataManager BatchJobDataManager { get; set; }


        public cBatchJobDataLoader(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
         )
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
            BatchJobDataManager = _BatchJobDataManager;
        }

        public void Init()
        {
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            ////////////// Global //////////////////

            for (int i = 0; i < BatchJobIDs.TypeList.Count; i++)
            {
                BatchJobDataManager.CreateBatchJobIfNotExists(BatchJobIDs.TypeList[i]);
            }

            List<cBatchJobEntity> __BatchJobList = BatchJobDataManager.GetBatchJobList();
            for (int i = 0; i < __BatchJobList.Count;i++)
            {
                if (BatchJobIDs.GetByCode(__BatchJobList[i].Code, null) == null)
                {
                    __BatchJobList[i].State = EBatchJobState.Stopped.ID;
                }
            }
            __DatabaseContext.SaveChanges();
        }
    }
}
