using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService
{
    public interface IDataService
    {
        public DbContext GetCoreEFDatabaseContext();
        public void Save();
        public void Migrate();
        //public void ComponentLoad();
        public void LoadDomainDefaultData();
        public void LoadSysDefaultData();
        public void LoadBatchJob();
        public TDbContext GetDatabaseContext<TDbContext>() where TDbContext : DbContext;
    }
}
