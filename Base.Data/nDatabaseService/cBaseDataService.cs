using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService
{
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
            DbContext __DatabaseContext = GetCoreEFDatabaseContext();
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

        public DbContext GetCoreEFDatabaseContext()
        {
            if (Activity.Current != null)
            {
                TDatabaseContext __Item = (TDatabaseContext)Activity.Current.GetTagItem("DatabaseContext");
                if (__Item == null)
                {
                    lock (Activity.Current)
                    {
                        __Item = (TDatabaseContext)Activity.Current.GetTagItem("DatabaseContext");
                        if (__Item == null)
                        {
                            Console.WriteLine(Activity.Current.Id.ToString());
                            __Item = (TDatabaseContext)ServiceContext.App.Factories.ObjectFactory.ResolveInstance<TDatabaseContext>();
                            Activity.Current.SetTag("DatabaseContext", __Item);
                        }
                    }
                }
                return __Item;
            }
            else
            {
                return (DbContext)ServiceContext.App.Factories.ObjectFactory.ResolveInstance<TDatabaseContext>();
            }
        }

        public void Save()
        {
            GetCoreEFDatabaseContext().SaveChanges();
        }

        public TCastDatabaseContext GetDatabaseContext<TCastDatabaseContext>() where TCastDatabaseContext : DbContext
        {
            return (TCastDatabaseContext)GetCoreEFDatabaseContext();
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

        public void LoadDomainDefaultData()
        {
            try
            {
                Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<IDomainDefaultDataLoader>();
                if (__Type != null)
                {
                    IDomainDefaultDataLoader __DefaultDataLoader = ServiceContext.App.Factories.ObjectFactory.ResolveInstance<IDomainDefaultDataLoader>();
                    if (__DefaultDataLoader != null) __DefaultDataLoader.Load();
                }
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
            }
        }

        public void LoadSysDefaultData()
        {
            try
            {
                Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<ISysDefaultDataLoader>();
                if (__Type != null)
                {
                    ISysDefaultDataLoader __DefaultDataLoader = ServiceContext.App.Factories.ObjectFactory.ResolveInstance<ISysDefaultDataLoader>();
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

   
    }
}
