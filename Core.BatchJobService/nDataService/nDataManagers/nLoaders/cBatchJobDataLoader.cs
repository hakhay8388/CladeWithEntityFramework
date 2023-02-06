using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDataService.nDataManagers;
using Sys.Boundary.nData;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDataService;
using Core.BatchJobService.nDefaultValueTypes;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBatchJobDataLoader : cBaseDataLoader
    {
        public cBatchJobDataManager BatchJobDataManager { get; set; }


        public cBatchJobDataLoader(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
         )
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
            BatchJobDataManager = _BatchJobDataManager;
        }

        public void Init()
        {
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
            DataService.Save();
        }
    }
}
