using Base.Data.nDatabaseService;
using Base.FileData;
using Bootstrapper.Core.nCore;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;
using Data.Domain.nDataService;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Core.BatchJobService.nBatchJobManager.nJobs
{
    public abstract class cBaseJob<TJobProps> : cCoreService<cDataServiceContext>, IBatchJob
        where TJobProps : cBaseJobProps
    {
        public  BatchJobIDs BatchJobID { get; set; }
        protected cDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobManager BatchJobManager { get; set; }

		public cBaseJob(BatchJobIDs _BatchJobID, cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService, cBatchJobDataManager _BatchJobDataManager)
          : base(_CoreServiceContext)
        {
            FileDataService = _FileDataService;
            BatchJobID = _BatchJobID;
            BatchJobDataManager = _BatchJobDataManager;
            DataService = _DataService;

        }

        public virtual void AddQueue(TJobProps _Props)
        {
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            cBatchJobEntity __BatchJobEntity = BatchJobDataManager.GetBatchJobByCode(BatchJobID.Code);

            __BatchJobEntity.JobExecutions.Add(new cBatchJobExecutionEntity() {
                        State = EBatchJobExecutionState.NotRunning.ID,
                        ParameterObjects = _Props.SerializeObject()
                });
            __DatabaseContext.SaveChanges();
        }
				
		public void Execute(cBatchJobEntity _BatchJobEntity, cBatchJobExecutionEntity _Entity)
        {
            TJobProps __JobProps = cBaseJobProps.GetPropFromString<TJobProps>(_Entity.ParameterObjects);
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            Stopwatch __StopWatch = new Stopwatch();
            __StopWatch.Start();

            try
            {
                __DatabaseContext.Perform(() =>
                {
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.State = EBatchJobExecutionState.Running.ID;
                    __DatabaseContext.SaveChanges();
                });

                cBatchJobResult __Result = Run(__JobProps);

                __StopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan __TimeSpan = __StopWatch.Elapsed;

                __DatabaseContext.Perform(() =>
                {
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.ElapsedTimeMilisecond = Convert.ToInt32(__TimeSpan.TotalMilliseconds);
                    _Entity.State = EBatchJobExecutionState.Success.ID;
                    _Entity.Result = __Result.Result;
                    __DatabaseContext.SaveChanges();
                });
            }
            catch(Exception _Ex)
            {
				App.Loggers.BatchJobLogger.LogError(_Ex);
				__StopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan __TimeSpan = __StopWatch.Elapsed;

                cBatchJobExecutionEntity __RetryBatchJobExecutionEntity = null;
                __DatabaseContext.Perform(() =>
                {
                    string __Ex = _Ex.Message + _Ex.StackTrace;
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.State = EBatchJobExecutionState.Error.ID;
                    _Entity.ElapsedTimeMilisecond = Convert.ToInt32(__TimeSpan.TotalMilliseconds);
                    _Entity.Exception = __Ex;
                    __DatabaseContext.SaveChanges();

                    if (_BatchJobEntity.MaxRetryCount > _Entity.CurrentTryCount)
                    {
                        _BatchJobEntity.JobExecutions.Add(new cBatchJobExecutionEntity() {
                            CurrentTryCount = _Entity.CurrentTryCount + 1,
                            ParameterObjects = _Entity.ParameterObjects,
                            State = EBatchJobExecutionState.NotRunning.ID
                        });

                        __DatabaseContext.SaveChanges();
                    }
                });

                if (__RetryBatchJobExecutionEntity != null)
                {
                    Execute(_BatchJobEntity, __RetryBatchJobExecutionEntity);
                }
            }
        }

        public abstract cBatchJobResult Run(TJobProps _Props);

    }
}
