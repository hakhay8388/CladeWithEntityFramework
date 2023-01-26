using Base.Data.nDatabaseService.nDatabase;
using Data.Boundary.nData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cBatchJobEntity : cBaseEntity<cBatchJobEntity>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int TimePeriodMilisecond { get; set; }

        public int State { get; set; }

        public bool ExecuteFirstWithoutWait { get; set; }

        public bool AutoAddExecution { get; set; }
        
        public bool StopAfterFirstExecution { get; set; }

        public int MaxRetryCount { get; set; }

        public List<cBatchJobExecutionEntity> JobExecutions { get; private set; }

    }
}
