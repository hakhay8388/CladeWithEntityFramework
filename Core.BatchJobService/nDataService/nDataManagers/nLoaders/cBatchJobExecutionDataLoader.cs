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
using Core.BatchJobService.nBatchJobManager.nJobs;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBatchJobExecutionDataLoader : cBaseDataLoader
    {
        public cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }

        public cBatchJobDataManager BatchJobDataManager { get; set; }


        public cBatchJobExecutionDataLoader(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager
         )
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
            BatchJobDataManager = _BatchJobDataManager;
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public override void Init()
        {
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            ////////////// Global //////////////////


            for (int i = 0; i < DefaultBatchJobExecutionIDs.TypeList.Count; i++)
            {
                DefaultBatchJobExecutionIDs __Excution = DefaultBatchJobExecutionIDs.TypeList[i];
                int __Count = BatchJobDataManager.GetBatchJobExecutionCount(__Excution.BatchJobID.Code);
                if (__Count < 1)
                {
                    
                    BatchJobExecutionDataManager.AddBatchJob(BatchJobDataManager.GetBatchJobByCode(__Excution.BatchJobID.Code), __Excution.Props.SerializeObject(), EBatchJobExecutionState.NotRunning, "", "", DateTime.Now, 0);
                }
            }
        }
    }
}
