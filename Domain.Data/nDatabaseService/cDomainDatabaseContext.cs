using Base.Data.nDatabaseService.nDatabase;

using Bootstrapper.Core.nApplication;
using System.Reflection;
using Sys.Data.nDatabaseService.nSystemEntities;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Sys.Data.nDatabaseService;
using Microsoft.EntityFrameworkCore;
using Domain.Data.nDatabaseService.nEntities;

namespace Domain.Data.nDatabaseService
{
    public class cDomainDatabaseContext : cSysDatabaseContext
    {

        public DbSet<cPaymentEntity> Payments { get; set; }

        public DbSet<cLawsuitEntity> Lawsuits { get; set; }
        

    }
}
