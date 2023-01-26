using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Boundary.nCore.nObjectLifeTime
{
    public enum LifeTime
    {
        ContainerControlledLifetimeManager = 1,
        ExternallyControlledLifetimeManager = 2,
        HierarchicalLifetimeManager = 3,
        PerResolveLifetimeManager = 4,
        PerThreadLifetimeManager = 5,
        TransientLifetimeManager = 6,
    }
    
}
