using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nBootstrapper;
using Bootstrapper.Core.nApplication.nConfiguration;
using Bootstrapper.Core.nApplication.nFactories;
using Bootstrapper.Core.nApplication.nFactories.nFormFactory;
using Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nHandlers;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;

namespace Bootstrapper.Core.nCore
{
    public class cCoreService<TServiceContext> : cCoreObject where TServiceContext : cCoreServiceContext
    {
        public TServiceContext ServiceContext { get; set; }
        public cCoreService(TServiceContext _ServiceContext)
            :base(_ServiceContext.App)
        {
            ServiceContext = _ServiceContext;
        }

    }
}
