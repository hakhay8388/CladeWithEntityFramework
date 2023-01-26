using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;

namespace Bootstrapper.Boundary.nCore.nObjectLifeTime
{
    public class LifeTimeEnumConverter
    {
        public static ITypeLifetimeManager GetLifetimeManager(LifeTime _ConstValue)
        {
            switch (_ConstValue)
            {
                case LifeTime.ContainerControlledLifetimeManager: return new ContainerControlledLifetimeManager();
                case LifeTime.ExternallyControlledLifetimeManager: return new ExternallyControlledLifetimeManager();
                case LifeTime.HierarchicalLifetimeManager: return new HierarchicalLifetimeManager();
                case LifeTime.PerResolveLifetimeManager: return new PerResolveLifetimeManager();
                case LifeTime.PerThreadLifetimeManager: return new PerThreadLifetimeManager();
                case LifeTime.TransientLifetimeManager: return new TransientLifetimeManager();
                default: return new ContainerControlledLifetimeManager();
            }
        }

        public static IFactoryLifetimeManager GetLifetimeManagerForFactory(LifeTime _ConstValue)
        {
            switch (_ConstValue)
            {
                case LifeTime.ContainerControlledLifetimeManager: return new ContainerControlledLifetimeManager();
                case LifeTime.ExternallyControlledLifetimeManager: return new ExternallyControlledLifetimeManager();
                case LifeTime.HierarchicalLifetimeManager: return new HierarchicalLifetimeManager();
                case LifeTime.PerResolveLifetimeManager: return new PerResolveLifetimeManager();
                case LifeTime.PerThreadLifetimeManager: return new PerThreadLifetimeManager();
                case LifeTime.TransientLifetimeManager: return new TransientLifetimeManager();
                default: return new ContainerControlledLifetimeManager();
            }
        }
    }
}
