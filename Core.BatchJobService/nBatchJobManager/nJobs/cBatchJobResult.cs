using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs
{
    public class cBatchJobResult
    {
        public string Result { get; set; }
        public cBatchJobResult(string _Result)
        {
            Result = _Result;
        }
    }
}
