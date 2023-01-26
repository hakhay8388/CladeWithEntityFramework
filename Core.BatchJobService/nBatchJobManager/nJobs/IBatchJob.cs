using Core.BatchJobService.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs
{
    public interface IBatchJob
    {
        cBatchJobManager BatchJobManager { get; set; }
        BatchJobIDs BatchJobID { get; set; }
        void Execute(cBatchJobEntity _BatchJobEntity, cBatchJobExecutionEntity _Entity);
    }
}
