using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Boundary.nData;
using Sys.Data.nDataService.nDataManagers;
using Sys.Data.nDataService;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    public class cBatchJobExecutionDataManager : cBaseDataManager
    {
        public cBatchJobExecutionDataManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

		public List<cBatchJobExecutionEntity> GetUnexecuted(cBatchJobEntity _BatchJobEntity)
		{
            return cBatchJobExecutionEntity.Get(__Item => __Item.BatchJob == _BatchJobEntity && __Item.State == EBatchJobExecutionState.NotRunning.ID).ToList();
		}

		public cBatchJobExecutionEntity GetLastExecution(cBatchJobEntity _BatchJobEntity)
        {
            return cBatchJobExecutionEntity.Get(__Item => __Item.BatchJob == _BatchJobEntity).OrderByDescending(__Item => __Item.ExecutionTime).FirstOrDefault();
        }

        public cBatchJobExecutionEntity AddBatchJob(cBatchJobEntity _OwnerBatchJobEntity, string _ParameterObjects, EBatchJobExecutionState _State, string _Exception, string _Result, DateTime _ExecutionTime, int _ElapsedTimeMilisecond)
        {
            cBatchJobExecutionEntity __BatchJobExecutionEntity = new cBatchJobExecutionEntity()
            {
                ParameterObjects = _ParameterObjects,
                State = _State.ID,
                Exception = _Exception,
                Result = _Result,
                ExecutionTime = _ExecutionTime,
                ElapsedTimeMilisecond = _ElapsedTimeMilisecond
            };

            _OwnerBatchJobEntity.JobExecutions.Add(__BatchJobExecutionEntity);
            __BatchJobExecutionEntity.Save();

            return __BatchJobExecutionEntity;
        }

        public int DeleteExecutionBeforeDate(DateTime _Date)
        {
            return  cBatchJobExecutionEntity.RemoveRange(__Item => __Item.ExecutionTime < _Date);
        }

    }
}
