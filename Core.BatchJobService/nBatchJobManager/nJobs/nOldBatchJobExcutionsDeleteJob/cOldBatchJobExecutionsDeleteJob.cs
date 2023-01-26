using Base.Data.nDatabaseService;
using Base.FileData;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService;
using Data.Domain.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob
{
    public class cOldBatchJobExecutionsDeleteJob : cBaseJob<cOldBatchJobExecutionsDeleteJobProps>
    {
        cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        public cOldBatchJobExecutionsDeleteJob(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager)
         : base(BatchJobIDs.OldBatchJobExcutionsDelete, _CoreServiceContext, _DataService, _FileDataService, _BatchJobDataManager)
        {
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public override cBatchJobResult Run(cOldBatchJobExecutionsDeleteJobProps _Props)
        {
            cDatabaseContext __DateService = DataService.GetDatabaseContext();

            int __DeleteCount = 0;
            __DateService.Perform(() =>
            {
                __DeleteCount = BatchJobExecutionDataManager.DeleteExecutionBeforeDate(DateTime.Now.AddDays(-_Props.KeepLastDayCount));
                __DateService.SaveChanges();
            });
            cBatchJobResult __Result = new cBatchJobResult("Son " + _Props.KeepLastDayCount + " gün öncesindeki BatchJob verileri silindi. Silinen Kayıt Sayısı : " + __DeleteCount);

            return __Result;
        }
    }
}
