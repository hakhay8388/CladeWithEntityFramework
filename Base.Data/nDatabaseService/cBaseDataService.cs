using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService
{
    [Register(null, false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public abstract class cBaseDataService<TDatabaseContext> : cCoreService<cDataServiceContext>, IDataService
        where TDatabaseContext : cBaseDatabaseContext
    {
        public bool IsMigrated { get; set; }
        public cBaseDataService(cDataServiceContext _ServiceContext)
          : base(_ServiceContext)
        {
            ServiceContext = _ServiceContext;
            IsMigrated = false;
            cBaseEntityType.DataService = this;
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterType(typeof(TDatabaseContext), typeof(TDatabaseContext), LifeTime.PerThreadLifetimeManager);
        }

        public void Migrate()
        {
            TDatabaseContext __DatabaseContext = GetDatabaseContext();
            if (!IsMigrated)
            {
                lock (this)
                {
                    if (!IsMigrated)
                    {
                        lock (this)
                        {
                            __DatabaseContext.Database.Migrate();
                            IsMigrated = true;
                        }
                        
                    }
                }
            }
        }

        public void ComponentLoad()
        {
            try
            {
                
                Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<IComponentLoader>();
                if (__Type != null)
                {
                    IComponentLoader __ComponentLoader = (IComponentLoader)App.Factories.ObjectFactory.ResolveInstance(__Type);
                    __ComponentLoader.Load();
                }
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
            }
        }

        public void LoadDefaultData()
        {
            try
            {
                Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<IDefaultDataLoader>();
                if (__Type != null)
                {
                    IDefaultDataLoader __DefaultDataLoader = ServiceContext.App.Factories.ObjectFactory.ResolveInstance<IDefaultDataLoader>();
                    if (__DefaultDataLoader != null) __DefaultDataLoader.Load();
                }
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
            }
        }

        public void LoadBatchJob()
        {
            try
            {
                Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<IBatchJobDataLoader>();
                if (__Type != null)
                {
                    IBatchJobDataLoader __BatchJobDataLoader = (IBatchJobDataLoader)App.Factories.ObjectFactory.ResolveInstance(__Type);
                    if (__BatchJobDataLoader != null) __BatchJobDataLoader.Load();
                }
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
            }
        }

        public TDatabaseContext GetDatabaseContext()
        {
            return ServiceContext.App.Factories.ObjectFactory.ResolveInstance<TDatabaseContext>();
        }

        public DbContext GetCoreEFDatabaseContext()
        {
            return (DbContext)GetDatabaseContext();
            
        }
    }
}
