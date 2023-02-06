using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nAttributes;

namespace Domain.Data.nDatabaseService
{
    [Register(typeof(IDataService), true, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cDomainDataService : cBaseDataService<cDomainDatabaseContext>
    {

        public cDomainDataService(cDataServiceContext _ServiceContext)
          : base(_ServiceContext)
        {
        }
    }
}
