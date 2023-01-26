using Base.Data.nDatabaseService.nDatabase;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cBatchJobExecutionEntity : cBaseEntity<cBatchJobExecutionEntity>
    {
        public string ParameterObjects { get; set; }

        public int State { get; set; }

        public string Exception { get; set; }

        public string Result { get; set; }

        public int CurrentTryCount { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ElapsedTimeMilisecond { get; set; }

        public cBatchJobEntity BatchJob { get; set; }

    }
}
