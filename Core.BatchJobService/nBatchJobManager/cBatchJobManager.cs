using Base.FileData;
using Core.BatchJobService.nBatchJobManager.nJobs;
using Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Data.Domain.nDataService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nCore;
using Base.Data.nDatabaseService;

namespace Core.BatchJobService.nBatchJobManager
{
    [Register(typeof(cBatchJobManager), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cBatchJobManager : cCoreService<cDataServiceContext>
    {
        public List<IBatchJob> JobList { get; set; }
        public cTestServiceJob TestServiceJob { get; set; }
        public cOldBatchJobExecutionsDeleteJob OldBatchJobExcutionsDeleteJob { get; set; }

        public cBatchJobManager(cDataServiceContext _CoreServiceContext
                , cTestServiceJob _TestServiceJob
                , cOldBatchJobExecutionsDeleteJob _OldBatchJobExcutionsDeleteJob

            )
              : base(_CoreServiceContext)
        {
            JobList = new List<IBatchJob>();
        }

        public override void Init()
        {


            Type __ThisType = this.GetType();
            List<Type> __Templates = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IBatchJob>();
            __Templates.ForEach(__Type =>
            {
                IBatchJob __Step = (IBatchJob)App.Factories.ObjectFactory.ResolveInstance(__Type);

                PropertyInfo __PropertyInfo = __ThisType.GetAllProperties().Where(__Item => __Item.Name.StartsWith(__Step.BatchJobID.Name)).FirstOrDefault();
                if (__PropertyInfo == null)
                {
                    throw new Exception($"{__Step.BatchJobID.Name} BatchJob ismi BatchJobIDs ile eşleşmiyor.");
                }
                __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __Step });

                __Step.BatchJobManager = this;
                JobList.Add(__Step);
            });
        }

        public IBatchJob GetBatchJobByCode(string _Code)
        {
            return JobList.Find(__Item => __Item.BatchJobID.Code == _Code);
        }
    }
}
