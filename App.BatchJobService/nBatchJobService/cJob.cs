using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Core.BatchJobService.nBatchJobManager;
using Core.BatchJobService.nBatchJobManager.nJobs;
using Core.BatchJobService.nDataService.nDataManagers;
using Data.Boundary.nData;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.BatchJobService.nBatchJobService
{
    public class cJob : cCoreObject
    {
        public bool Executed = false;
        public bool Finished = false;
        public EBatchJobState JobState { get; set; }
        public Thread ExecuterThread { get; set; }
        public cBatchJobEntity BatchJobEntity { get; set; }
        public cBatchJobEntity RefreshedBatchJobEntity { get; set; }
        public cDataService DataService { get; set; }
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        public cBatchJobManager BatchJobManager { get; set; }
        public cJob(
            cApp _App
            , cDataService _DataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager,
            cBatchJobManager _BatchJobManager
         )
            : base(_App)
        {
            BatchJobDataManager = _BatchJobDataManager;
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
            DataService = _DataService;
            BatchJobManager = _BatchJobManager;
        }

        public void Start()
        {
            Finished = false;
            ExecuterThread = new Thread(new ThreadStart(Execute));
            ExecuterThread.Start();
        }

        public void Execute()
        {
            cDatabaseContext __DataService = DataService.GetDatabaseContext();
            IBatchJob __BatchJob = BatchJobManager.GetBatchJobByCode(BatchJobEntity.Code);
            if (__BatchJob == null)
            {
				List<string> __List = new List<string>();
				__List.Add("###########################################");
				__List.Add($"#####  Batch Job Bulunamadı {BatchJobEntity.Code}  ############");
				__List.Add("###########################################");
				App.Loggers.BatchJobLogger.LogError(__List, null, null);

                return;
            }
            while (true)
            {
                if (RefreshedBatchJobEntity != null)
                {
                    BatchJobEntity = RefreshedBatchJobEntity;
                }

                try
                {
                    if (Finished) break;
                    List<cBatchJobExecutionEntity> __Unexecuted = null;
                    if (BatchJobEntity.AutoAddExecution && !BatchJobEntity.StopAfterFirstExecution)
                    {
                        cBatchJobExecutionEntity __LastExecution = BatchJobExecutionDataManager.GetLastExecution(BatchJobEntity);
                        if (__LastExecution != null)
                        {
                            if (__LastExecution.State == EBatchJobExecutionState.NotRunning.ID)
                            {
                                __Unexecuted = new List<cBatchJobExecutionEntity>() { __LastExecution };
                            }
                            else
                            {
                                cBatchJobExecutionEntity __BatchJobExecutionEntity = null;
                                __DataService.Perform(() =>
                                {
                                    __BatchJobExecutionEntity = new cBatchJobExecutionEntity()
                                    {
                                        ParameterObjects = __LastExecution.ParameterObjects
                                    };
                                    BatchJobEntity.JobExecutions.Add(__BatchJobExecutionEntity);
                                    __DataService.SaveChanges();
                                });
                                __Unexecuted = new List<cBatchJobExecutionEntity>() { __BatchJobExecutionEntity };
                            }
                        }
                        else
                        {

                            __Unexecuted = new List<cBatchJobExecutionEntity>();
                        }
                    }
                    else
                    {
						__Unexecuted = BatchJobExecutionDataManager.GetUnexecuted(BatchJobEntity);
					}

                    if (BatchJobEntity.StopAfterFirstExecution)
                    {
                        if (!Executed)
                        {
                            if (BatchJobEntity.ExecuteFirstWithoutWait)
                            {
                                __Unexecuted.ForEach(__Item =>
                                {
                                    __BatchJob.Execute(BatchJobEntity, __Item); //HangFire kullanmak istemzsek
                                    Executed = true;
                                });
                            }
                            else
                            {
                                Thread.Sleep(BatchJobEntity.TimePeriodMilisecond);
                                __Unexecuted.ForEach(__Item =>
                                {
                                    __BatchJob.Execute(BatchJobEntity, __Item); //HangFire kullanmak istemzsek
                                    Executed = true;
                                });
                            }
                        }
                        if (Executed)
                        {
                            __DataService.Perform(() =>
                            {
                                BatchJobEntity.State = EBatchJobState.Stopped.ID;
                                __DataService.SaveChanges();
                            });
                            Finished = true;
                            break;
                        }
                        Thread.Sleep(BatchJobEntity.TimePeriodMilisecond);
                    }
                    else
                    {
                        if (!BatchJobEntity.ExecuteFirstWithoutWait)
                        {
                            Thread.Sleep(BatchJobEntity.TimePeriodMilisecond);
                        }

                        __Unexecuted.ForEach(__Item =>
                        {
                            __BatchJob.Execute(BatchJobEntity, __Item);// HangFire kullanmak istemzsek
                        });

                        if (BatchJobEntity.ExecuteFirstWithoutWait)
                        {
                            Thread.Sleep(BatchJobEntity.TimePeriodMilisecond);
                        }
                    }

                }
                catch (Exception _Ex)
                {
					List<string> __List = new List<string>();
					__List.Add("###########################################");
					__List.Add("#####  Batch  Execution Error  ############");
					__List.Add("###########################################");
					App.Loggers.BatchJobLogger.LogError(__List, _Ex, null);
                    Thread.Sleep(10000);
                }
            }
        }

        public void Refresh(cBatchJobEntity _Item)
        {
            RefreshedBatchJobEntity = _Item;
        }

        public void Stop()
        {
            Executed = false;
            Finished = true;
        }
    }
}
