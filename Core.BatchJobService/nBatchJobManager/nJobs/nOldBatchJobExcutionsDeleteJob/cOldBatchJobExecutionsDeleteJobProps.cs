using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob
{
    public class cOldBatchJobExecutionsDeleteJobProps : cBaseJobProps
    {
        public virtual int KeepLastDayCount { get; set; }
    }
}
