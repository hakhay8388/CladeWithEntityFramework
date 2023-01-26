using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Base.Data.nDatabaseService;
using Base.FileData.nConfiguration;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Core.BatchJobService.nDataService.nDataManagers;
using Data.Boundary.nData;
using Data.Domain.nDatabaseService.nSystemEntities;
using Data.Domain.nDataService.nDataManagers;


namespace App.BatchJobService.nBatchJobService
{
    public class cBatchJobService : cCoreService<cDataServiceContext>
    {
        bool Exiting { get; set; }
        Thread JobStateControllerThread { get; set; }
        List<cJob> JobList { get; set; }
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobService(cDataServiceContext _CoreServiceContext, cBatchJobDataManager _BatchJobDataManager)
            : base(_CoreServiceContext)
        {
            BatchJobDataManager = _BatchJobDataManager;
            JobList = new List<cJob>();
            Exiting = false;
        }

        public void Start()
        {
            Exiting = false;
            JobStateControllerThread = new Thread(new ThreadStart(JobStateController));
            JobStateControllerThread.Start();
        }

        public void JobStateController()
        {
            while (true)
            {
                try
                {
                    if (Exiting) break;
                    List<cBatchJobEntity> __BatchJobEntityList = BatchJobDataManager.GetBatchJobList();

                    __BatchJobEntityList.ForEach(__Item =>
                    {
                        if (__Item.State == EBatchJobState.Started.ID)
                        {
                            cJob __Job = JobList.Find(__JobItem => __JobItem.BatchJobEntity.ID == __Item.ID);
                            if (__Job == null)
                            {
                                __Job = App.Factories.ObjectFactory.ResolveInstance<cJob>();
                                __Job.BatchJobEntity = __Item;
                                __Job.Start();
                                JobList.Add(__Job);
                            }
                            else
                            {
                                var __Temp = __Job.ExecuterThread.ThreadState;
                                if (__Job.ExecuterThread.ThreadState != ThreadState.Running && __Job.ExecuterThread.ThreadState != ThreadState.WaitSleepJoin)
                                {
                                    __Job.Stop();
                                    JobList.Remove(__Job);
                                }
                                else
                                {
                                    __Job.Refresh(__Item);
                                }
                            }
                        }
                        else if (__Item.State == EBatchJobState.Stopped.ID)
                        {
                            cJob __JobItem = JobList.Find(__JobItem => __JobItem.BatchJobEntity.ID == __Item.ID);
                            if (__JobItem != null)
                            {
                                __JobItem.Stop();
                                JobList.Remove(__JobItem);
                            }

                        }
                    });

                    Thread.Sleep(10000);
                }
                catch(Exception _Ex)
                {
					List<string> __List = new List<string>();
					__List.Add("###########################################");
					__List.Add("###### BatchJob Service Error  ############");
					__List.Add("###########################################");
                    App.Loggers.BatchJobLogger.LogError(__List, _Ex, null);
                    Thread.Sleep(10000);
                }
            }
        }

        public void Stop()
        {
            Exiting = true;
            JobList.ForEach(__Item =>
            {
                __Item.Stop();
            });
        }
    }
}
